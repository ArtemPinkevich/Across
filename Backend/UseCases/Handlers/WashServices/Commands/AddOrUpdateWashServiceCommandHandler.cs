namespace UseCases.Handlers.WashServices.Commands
{
    using DataAccess.Interfaces;
    using Entities;
    using MediatR;
    using AutoMapper;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Dto;

    public class AddOrUpdateWashServiceCommandHandler : IRequestHandler<AddOrUpdateWashServiceCommand, AddOrUpdateWashServiceResultDto>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<WashService> _washServiceRepository;
        private readonly IRepository<ComplexWashService> _complexWashServiceRepository;
        private readonly IRepository<PriceGroup> _priceGroupRepository;
        private readonly IRepository<WashServiceSettings> _wasServiceSettingsRepository;
        private readonly IRepository<CarWash> _carWashRepository;

        public AddOrUpdateWashServiceCommandHandler(IMapper mapper,
            IRepository<WashService> washServiceRepository,
            IRepository<ComplexWashService> complexWashServiceRepository,
            IRepository<PriceGroup> priceGroupRepository,
            IRepository<WashServiceSettings> wasServiceSettingsRepository,
            IRepository<CarWash> carWashRepository)
        {
            _mapper = mapper;
            _complexWashServiceRepository = complexWashServiceRepository;
            _washServiceRepository = washServiceRepository;
            _priceGroupRepository = priceGroupRepository;
            _wasServiceSettingsRepository = wasServiceSettingsRepository;
            _carWashRepository = carWashRepository;
        }

        public async Task<AddOrUpdateWashServiceResultDto> Handle(AddOrUpdateWashServiceCommand request, CancellationToken cancellationToken)
        {
            if (! await IsValidatRequest(request.WashService, request.CarWashId))
                return null;

            var washService = (request.WashService.Composition?.Count ?? 0) == 0
                ? await AddOrUpdateWashService(request.WashService, request.CarWashId)
                : await AddOrUpdateComplexWashService(request.WashService, request.CarWashId);

            await _washServiceRepository.SaveAsync();
            return new AddOrUpdateWashServiceResultDto() { WashService = _mapper.Map<WashServiceDto>(washService) };
        }

        private async Task<bool> IsValidatRequest(WashServiceDto washServiceDto, int carWashId)
        {
            if (washServiceDto.WashServiceSettingsDtos == null)
                return false;
            
            foreach (var priceGroup in washServiceDto.WashServiceSettingsDtos)
                if (await _priceGroupRepository.GetAsync(x => x.Id == priceGroup.PriceGroupId) == null)
                    return false;

            if (await _carWashRepository.GetAsync(x => x.Id == carWashId) == null)
                return false;
            
            return true;
        }
        
        private async Task<WashService> AddOrUpdateWashService(WashServiceDto washServiceDto, int carWashId)
        {
            var washService = await _washServiceRepository.GetAsync(x => x.Id == washServiceDto.Id);
            if (washService == null)
            {
                washService = _mapper.Map<WashService>(washServiceDto,
                    opt => opt.AfterMap((src, dest) => dest.CarWashId = carWashId));
                await _washServiceRepository.AddAsync(new List<WashService>() { washService });
            }
            else
            {
                await UpdateBaseParams(washService, washServiceDto);
                await _washServiceRepository.UpdateAsync(washService);
            }

            return washService;
        }
        
        private async Task<WashService> AddOrUpdateComplexWashService(WashServiceDto washServiceDto, int carWashId)
        {
            var washService = await _complexWashServiceRepository.GetAsync(x => x.Id == washServiceDto.Id);
            if (washService == null)
            {
                washService = _mapper.Map<ComplexWashService>(washServiceDto,
                    opt => opt.AfterMap((src, dest) => dest.CarWashId = carWashId)); 
                await _complexWashServiceRepository.AddAsync(new List<ComplexWashService>() { washService });
            }
            else
            {
                await UpdateBaseParams(washService, washServiceDto);
                await _complexWashServiceRepository.UpdateAsync(washService);
            }

            List<WashService> baseWashServices = await _washServiceRepository.GetAllAsync(o => washServiceDto.Composition.Contains(o.Id));
            washService.WashServices = baseWashServices;

            return washService;
        }

        private async Task UpdateBaseParams(WashService washService, WashServiceDto washServiceDto)
        {
            washService.Description = washServiceDto.Description;
            washService.Enabled = washServiceDto.Enabled;
            washService.Name = washServiceDto.Name;
            
            await _wasServiceSettingsRepository.DeleteAsync(x => x.WashServiceId == washService.Id);
            foreach (var washServiceSettings in washServiceDto.WashServiceSettingsDtos)
            {
                washService.WashServiceSettingsList.Add(_mapper.Map<WashServiceSettings>(washServiceSettings,
                    opt => opt.AfterMap((src, dest) => dest.WashServiceId = washService.Id)));
            }
        }
    }
}
