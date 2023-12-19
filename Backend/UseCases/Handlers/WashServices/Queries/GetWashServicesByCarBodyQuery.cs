namespace UseCases.Handlers.WashServices.Queries
{
    using MediatR;
    using System.Collections.Generic;
    using UseCases.Handlers.WashServices.Dto;

    public class GetWashServicesByCarBodyQuery : IRequest<List<WashServiceOnePriceDto>>
    {
        public int CarWashId { get; }
        public int CarBodyId { set; get; }

        public GetWashServicesByCarBodyQuery(int carWashId, int carBodyId)
        {
            CarWashId = carWashId;
            CarBodyId = carBodyId;
        }
    }
}
