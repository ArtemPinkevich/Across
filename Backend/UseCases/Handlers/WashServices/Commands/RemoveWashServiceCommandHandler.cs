namespace UseCases.Handlers.WashServices.Commands
{
    using DataAccess.Interfaces;
    using Entities;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;
    using UseCases.Handlers.WashServices.Dto;

    public class RemoveWashServiceCommandHandler : IRequestHandler<RemoveWashServiceCommand, RemoveWashServiceResultDto>
    {
        private readonly IRepository<WashService> _washServiceRepository;
        private readonly IRepository<ComplexWashService> _complexWashServiceRepository;
        private readonly IRepository<WashServiceSettings> _washServiceSettingsRepository;

        public RemoveWashServiceCommandHandler(IRepository<WashService> washServiceRepository,
            IRepository<ComplexWashService> complexWashServiceRepository,
            IRepository<WashServiceSettings> washServiceSettingsRepository)
        {
            _washServiceRepository = washServiceRepository;
            _complexWashServiceRepository = complexWashServiceRepository;
            _washServiceSettingsRepository = washServiceSettingsRepository;
        }

        public async Task<RemoveWashServiceResultDto> Handle(RemoveWashServiceCommand request, CancellationToken cancellationToken)
        {
            RemoveWashServiceResultDto removeWashServiceResultDto = new RemoveWashServiceResultDto() { RemovedWashServiceId = request.RemovedWashServiceId };

            if (await _complexWashServiceRepository.GetAsync(o => o.Id == request.RemovedWashServiceId) != null)
                await _complexWashServiceRepository.DeleteAsync(o => o.Id == request.RemovedWashServiceId);
            else if (await _washServiceRepository.GetAsync(o => o.Id == request.RemovedWashServiceId) != null)
                await _washServiceRepository.DeleteAsync(o => o.Id == request.RemovedWashServiceId);
            
            await _washServiceSettingsRepository.DeleteAsync(x => x.WashServiceId == request.RemovedWashServiceId);
            await _complexWashServiceRepository.SaveAsync();
            return removeWashServiceResultDto;
        }
    }
}
