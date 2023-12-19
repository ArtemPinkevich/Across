namespace UseCases.Handlers.WashServices.Queries
{
    using AutoMapper;
    using DataAccess.Interfaces;
    using Entities;
    using MediatR;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using UseCases.Handlers.WashServices.Dto;

    public class GetWashServicesQueryHandler : IRequestHandler<GetWashServicesQuery, GetWashServicesDto>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<WashService> _washServiceRepository;

        public GetWashServicesQueryHandler(IMapper mapper,
            IRepository<WashService> washServiceRepository)
        {
            _mapper = mapper;
            _washServiceRepository = washServiceRepository;
        }

        public async Task<GetWashServicesDto> Handle(GetWashServicesQuery request, CancellationToken cancellationToken)
        {
            List<WashService> washServices = request.IncludeDisabled 
                ? await _washServiceRepository.GetAllAsync(o => o.CarWashId == request.CarWashId)
                : await _washServiceRepository.GetAllAsync(o => o.CarWashId == request.CarWashId && o.Enabled);

            return _mapper.Map<GetWashServicesDto>(washServices);
        }
    }
}
