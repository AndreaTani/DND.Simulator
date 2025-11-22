using DND.Application.Contracts;
using DND.Domain.SharedKernel;

namespace DND.Application.Handlers
{
    public class CreatureTemporaryDamageModificationAppliedEventHandler : IDomainEventHandler<CreatureTemporaryDamageModificationAppliedEvent>
    {
        private readonly ICreatureService _creatureService;
        private readonly ILoggingService _loggingService;

        public CreatureTemporaryDamageModificationAppliedEventHandler(ICreatureService creatureService, ILoggingService loggingService)
        {
            _creatureService = creatureService;
            _loggingService = loggingService;
        }

        public async Task HandleAsync(CreatureTemporaryDamageModificationAppliedEvent domainEvent)
        {
            await _creatureService.AddTemporaryDamageModificationAsync(domainEvent.CreatureId, domainEvent.SourceId, domainEvent.Modification);

            string logMessage = $"A temporary modifier of '{domainEvent.Modification.Modifier}' for damage type '{domainEvent.Modification.TypeOfDamage}' has been applied to creature name {domainEvent.Modification.Name} (ID:{domainEvent.CreatureId}) from the followind source id: '{domainEvent.SourceId}'";
            await _loggingService.LogMessageAsync(logMessage);
        }
    }
}
