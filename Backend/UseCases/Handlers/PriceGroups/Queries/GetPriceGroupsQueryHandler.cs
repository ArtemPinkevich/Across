namespace UseCases.Handlers.PriceGroups.Queries
{
    using AutoMapper;
    using DataAccess.Interfaces;
    using Entities;
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using UseCases.Handlers.PriceGroups.Dto;

    public class GetPriceGroupsQueryHandler : IRequestHandler<GetPriceGroupsQuery, GetPriceGroupsResultDto>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<PriceGroup> _priceGroupRepository;

        public GetPriceGroupsQueryHandler(IMapper mapper, IRepository<PriceGroup> priceGroupRepository)
        {
            _mapper = mapper;
            _priceGroupRepository = priceGroupRepository;
        }

        public async Task<GetPriceGroupsResultDto> Handle(GetPriceGroupsQuery request, CancellationToken cancellationToken)
        {
            List<PriceGroup> priceGroup = await _priceGroupRepository.GetAllAsync(o => o.CarWashId == request.CarWashId);

            return _mapper.Map<GetPriceGroupsResultDto>(priceGroup);
        }
    }
}
