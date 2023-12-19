namespace UseCases.Handlers.CarWash.Queries
{
    using DataAccess.Interfaces;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;
    using UseCases.Handlers.CarWash.Dto;
    using Entities;
    using AutoMapper;

    public class GetCarWashSettingsQueryHandler : IRequestHandler<GetCarWashSettingsQuery, CarWashDto>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<CarWash> _carWashesRepository;

        public GetCarWashSettingsQueryHandler(IMapper mapper, IRepository<CarWash> carWashesRepository)
        {
            _mapper = mapper;
            _carWashesRepository = carWashesRepository;
        }

        public async Task<CarWashDto> Handle(GetCarWashSettingsQuery request, CancellationToken cancellationToken)
        {
            CarWash carWash = await _carWashesRepository.GetAsync(o => o.Id == request.CarWashId);
            return _mapper.Map<CarWashDto>(carWash);
        }
    }
}
