namespace UseCases.Handlers.Vehicles.Queries
{
    using AutoMapper;
    using DataAccess.Interfaces;
    using Entities;
    using MediatR;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using UseCases.Handlers.Vehicles.Dto;

    public class GetCarBodiesQueryHandler : IRequestHandler<GetCarBodiesQuery, List<CarBodyDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<CarBody> _carBodyRepository;

        public GetCarBodiesQueryHandler(IMapper mapper, IRepository<CarBody> carBodyRepository)
        {
            _carBodyRepository = carBodyRepository;
            _mapper = mapper;
        }

        public async Task<List<CarBodyDto>> Handle(GetCarBodiesQuery request, CancellationToken cancellationToken)
        {
            List<CarBody> carBodies = await _carBodyRepository.GetAllAsync();
            List<CarBodyDto> carBodiesDto = carBodies.Select(o => _mapper.Map<CarBodyDto>(o)).ToList();
            return carBodiesDto;
        }
    }
}
