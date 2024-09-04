using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Interfaces;
using MediatR;
using UseCases.Handlers.Cargo.Dto;
using UseCases.Handlers.Common.Dto;

namespace UseCases.Handlers.TransportationOrder.Queries;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, TransportationOrdersListDto>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Entities.TransportationOrder> _ordersRepository;

    public GetOrderByIdQueryHandler(IRepository<Entities.TransportationOrder> ordersRepository, IMapper mapper)
    {
        _mapper = mapper;
        _ordersRepository = ordersRepository;
    }

    public async Task<TransportationOrdersListDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var transportationOrders = await _ordersRepository.GetAllAsync(x => x.Id == request.OrderId);
        if (transportationOrders == null || transportationOrders.Count <= 0)
        {
            return new TransportationOrdersListDto()
            {
                Result = new TransportationOrderResult()
                {
                    Result = ApiResult.Failed,
                    Reasons = new[] { $"no orders found with id {request.OrderId}" }
                }
            };
        }

        return new TransportationOrdersListDto()
        {
            Result = new TransportationOrderResult() { Result = ApiResult.Success, },
            TransportationOrderDtos =
                transportationOrders.Select(x => _mapper.Map<TransportationOrderDto>(x)).ToList()
        };
    }
}