using DND.Application.Contracts;
using DND.Domain.SharedKernel;

namespace DND.Application.Handlers
{
    public class RecordDeathSaveRollHandler : IDomainEventHandler<CreatureDeathSaveRolledEvent>
    {
        private readonly IDeathSaveManagerService _deathSaveManagerService;
        private readonly ILoggingService _loggingService;

        public RecordDeathSaveRollHandler(IDeathSaveManagerService deathSaveManagerService, ILoggingService loggingService  )
        {
            _deathSaveManagerService = deathSaveManagerService;
            _loggingService = loggingService;
        }

        public async Task HandleAsync( CreatureDeathSaveRolledEvent domainEvent)
        {
            var logMessage = $"Recorded death save roll of {domainEvent.RollValue} for creature {domainEvent.CreatureName} (ID: {domainEvent.CreatureId})";

            await _deathSaveManagerService.RecordDeathSaveRollAsync(
                domainEvent.CreatureId,
                domainEvent.RollValue
            );

            await _loggingService.LogMessageAsync(logMessage);
        }
    }
}

