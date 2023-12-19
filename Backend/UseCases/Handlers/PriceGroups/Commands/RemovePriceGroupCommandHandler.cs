namespace UseCases.Handlers.WashServices.Commands
{
    using DataAccess.Interfaces;
    using Entities;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;
    using UseCases.Handlers.PriceGroups.Dto;

    public class RemovePriceGroupCommandHandler : IRequestHandler<RemovePriceGroupCommand, RemovePriceGroupResultDto>
    {
        private readonly IRepository<PriceGroup> _priceGroupRepository;

        public RemovePriceGroupCommandHandler(IRepository<PriceGroup> washServiceRepository)
        {
            _priceGroupRepository = washServiceRepository;
        }

        public async Task<RemovePriceGroupResultDto> Handle(RemovePriceGroupCommand request, CancellationToken cancellationToken)
        {
            RemovePriceGroupResultDto removePriceGroupResultDto = new RemovePriceGroupResultDto() { RemovedPriceGroupId = request.RemovedPriceGroupId };

            var priceGroup = await _priceGroupRepository.GetAsync(o => o.Id == request.RemovedPriceGroupId);
            if (priceGroup != null)
            {
                await _priceGroupRepository.DeleteAsync(o => o.Id == request.RemovedPriceGroupId);
                await _priceGroupRepository.SaveAsync();
                return removePriceGroupResultDto;
            }

            return removePriceGroupResultDto;
        }
    }
}
