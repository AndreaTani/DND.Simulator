using DND.Application.Contracts;
using DND.Domain.SharedKernel;

namespace DND.Application.Handlers
{
    public class CreatureTemporaryDamageModificationRemovedEventHandler : IDomainEventHandler<CreatureTemporaryDamageModificationRemovedEvent>
    {
        private readonly ICreatureService _creatureService;
        private readonly ILoggingService _loggingService;

        public CreatureTemporaryDamageModificationRemovedEventHandler(ICreatureService creatureService, ILoggingService loggingService)
        {
            _creatureService = creatureService;
            _loggingService = loggingService;
        }

        public async Task HandleAsync(CreatureTemporaryDamageModificationRemovedEvent domainEvent)
        {
            await _creatureService.RemoveTemporaryDamageModificationAsync(domainEvent.CreatureId, domainEvent.CreatureName, domainEvent.DamageType);

            string logMessage = $"The temporary damage modification rule of damage type '{domainEvent.DamageType}' hase been removed from creature {domainEvent.CreatureName} (ID: {domainEvent.CreatureId})";
            await _loggingService.LogMessageAsync(logMessage);
        }
    }
}
