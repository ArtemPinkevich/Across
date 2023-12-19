namespace UseCases.Handlers.CarWash.Queries
{
    using DataAccess.Interfaces;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;
    using UseCases.Handlers.CarWash.Dto;
    using Entities;
    using System.Collections.Generic;
    using AutoMapper;

    public class GetCarWashesQueryHandler : IRequestHandler<GetCarWashesQuery, CarWashesListResultDto>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<CarWash> _carWashesRepository;

        public GetCarWashesQueryHandler(IMapper mapper, IRepository<CarWash> carWashesRepository)
        {
            _mapper = mapper;
            _carWashesRepository = carWashesRepository;
        }

        public async Task<CarWashesListResultDto> Handle(GetCarWashesQuery request, CancellationToken cancellationToken)
        {
            var carWashes = await _carWashesRepository.GetAllAsync();
            return _mapper.Map<CarWashesListResultDto>(carWashes);
        }
    }
}
