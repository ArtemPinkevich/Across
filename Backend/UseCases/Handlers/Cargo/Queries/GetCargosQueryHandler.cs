using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Interfaces;
using MediatR;
using UseCases.Handlers.Cargo.Dto;

namespace UseCases.Handlers.Cargo.Queries;

public class GetCargosQueryHandler: IRequestHandler<GetCargosQuery, CargosListDto>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Entities.Cargo> _repository;
    
    public GetCargosQueryHandler(IRepository<Entities.Cargo> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<CargosListDto> Handle(GetCargosQuery request, CancellationToken cancellationToken)
    {
        var cargos = await _repository.GetAllAsync(x => x.UserId == request.UserId);

        return new CargosListDto()
        {
            Result = new CargoResult()
            {
                Result = Result.Ok,
            },
            Cargos = cargos.Select(x => _mapper.Map<CargoDto>(x)).ToList()
        };
    }
}