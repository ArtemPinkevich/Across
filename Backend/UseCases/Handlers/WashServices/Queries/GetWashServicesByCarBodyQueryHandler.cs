namespace UseCases.Handlers.WashServices.Queries
{
    using DataAccess.Interfaces;
    using Entities;
    using MediatR;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using UseCases.Handlers.WashServices.Dto;


    public class GetWashServicesByCarBodyQueryHandler : IRequestHandler<GetWashServicesByCarBodyQuery, List<WashServiceOnePriceDto>>
    {
        private readonly IRepository<WashService> _washServiceRepository;

        public GetWashServicesByCarBodyQueryHandler(IRepository<WashService> washServiceRepository)
        {
            _washServiceRepository = washServiceRepository;
        }

        public async Task<List<WashServiceOnePriceDto>> Handle(GetWashServicesByCarBodyQuery request, CancellationToken cancellationToken)
        {
            List<WashService> washServices = await _washServiceRepository.GetAllAsync(o => o.CarWashId == request.CarWashId && o.Enabled);

            List<WashServiceOnePriceDto> result = new List<WashServiceOnePriceDto>();
            foreach (var washService in washServices)
            {
                var washServiceOnePriceDto = new WashServiceOnePriceDto()
                {
                    Id = washService.Id,
                    Enabled = washService.Enabled,
                    Name = washService.Name,
                    Description = washService.Description,
                };

                if (washService is ComplexWashService complexWashService)
                {
                    washServiceOnePriceDto.Composition = complexWashService.WashServices?.Select(o => o.Id).ToList();
                }

                var washServiceSettings = washService.WashServiceSettingsList.FirstOrDefault(o => o.PriceGroup != null && o.Enabled && o.PriceGroup.CarBodies != null && o.PriceGroup.CarBodies.Any(c => c.Id == request.CarBodyId));
                if (washServiceSettings == null)
                {
                    continue;
                }

                washServiceOnePriceDto.Price = washServiceSettings.Price;
                washServiceOnePriceDto.Duration = washServiceSettings.DurationMinutes;

                result.Add(washServiceOnePriceDto);

            }

            return result;
        }
    }
}
