using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Interfaces;
using MediatR;
using UseCases.Handlers.Cargo.Dto;
using UseCases.Handlers.Common.Dto;

namespace UseCases.Handlers.TransportationOrder.Queries;

public class GetCargosQueryHandler: IRequestHandler<GetTransportationOrdersQuery, TransportationOrdersListDto>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Entities.TransportationOrder> _repository;
    
    public GetCargosQueryHandler(IRepository<Entities.TransportationOrder> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<TransportationOrdersListDto> Handle(GetTransportationOrdersQuery request, CancellationToken cancellationToken)
    {
        var transportationOrders = await _repository.GetAllAsync(x => x.UserId == request.UserId);

        return new TransportationOrdersListDto()
        {
            Result = new TransportationOrderResult()
            {
                Result = ApiResult.Success,
            },
            TransportationOrderDtos = transportationOrders.Select(x => _mapper.Map<TransportationOrderDto>(x)).ToList()
        };
    }
}