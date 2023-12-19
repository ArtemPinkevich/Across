namespace UseCases.Handlers.PriceGroups.Commands
{
    using AutoMapper;
    using DataAccess.Interfaces;
    using Entities;
    using MediatR;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using UseCases.Handlers.PriceGroups.Dto;

    public class AddOrUpdatePriceGroupCommandHandler : IRequestHandler<AddOrUpdatePriceGroupCommand, AddOrUpdatePriceGroupResultDto>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<PriceGroup> _priceGroupRepository;
        private readonly IRepository<CarBody> _carBodyRepository;

        public AddOrUpdatePriceGroupCommandHandler(IMapper mapper, IRepository<PriceGroup> priceGroupRepository, IRepository<CarBody> carBodyRepository)
        {
            _mapper = mapper;
            _priceGroupRepository = priceGroupRepository;
            _carBodyRepository = carBodyRepository;
        }

        public async Task<AddOrUpdatePriceGroupResultDto> Handle(AddOrUpdatePriceGroupCommand request, CancellationToken cancellationToken)
        {
            PriceGroupDto priceGroupDto = request.PriceGroup.Id == 0
                ? await AddPriceGroup(request.PriceGroup, request.CarWashId)
                : await UpdatePriceGroup(request.PriceGroup, request.CarWashId);

            await _priceGroupRepository.SaveAsync();
            return new AddOrUpdatePriceGroupResultDto() { PriceGroup = priceGroupDto };
        }

        private async Task<PriceGroupDto> AddPriceGroup(PriceGroupDto priceGroupDto, int carWashId)
        {
            var priceGroup = new PriceGroup();
            await UpdatePriceGroup(priceGroup, priceGroupDto, carWashId);

            await _priceGroupRepository.AddAsync(new List<PriceGroup>() { priceGroup });

            var resultPriceGroupDto = _mapper.Map<PriceGroupDto>(priceGroup);
            return resultPriceGroupDto;
        }

        private async Task<PriceGroupDto> UpdatePriceGroup(PriceGroupDto priceGroupDto, int carWashId)
        {
            var priceGroup = await _priceGroupRepository.GetAsync(o => o.Id == priceGroupDto.Id);
            if (priceGroup == null)
            {
                await _priceGroupRepository.DeleteAsync(o => o.Id == priceGroupDto.Id);
                return await AddPriceGroup(priceGroupDto, carWashId);
            }

            await UpdatePriceGroup(priceGroup, priceGroupDto, carWashId);
            await _priceGroupRepository.UpdateAsync(priceGroup);

            var resultPriceGroupDto = _mapper.Map<PriceGroupDto>(priceGroup);
            return resultPriceGroupDto;
        }

        private async Task UpdatePriceGroup(PriceGroup priceGroup, PriceGroupDto priceGroupDto, int carWashId)
        {
            var carBodiesIds = priceGroupDto.CarBodies.Select(o => o.CarBodyId);
            var carBodies = await _carBodyRepository.GetAllAsync(o => carBodiesIds.Contains(o.Id));

            priceGroup.Name = priceGroupDto.Name;
            priceGroup.Description = priceGroupDto.Description;
            priceGroup.CarBodies = carBodies;
            priceGroup.CarWashId = carWashId;
        }
    }
}
