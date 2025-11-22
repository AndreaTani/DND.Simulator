using DND.Application.Contracts;
using DND.Domain.SharedKernel;

namespace DND.Application.Handlers
{
    public class CreatureTemporaryDamageImmunityRemovedEventHandler : IDomainEventHandler<CreatureTemporaryDamageImmunityRemovedEvent>
    {
        private readonly ICreatureService _creatureService;
        private readonly ILoggingService _loggingService;

        public CreatureTemporaryDamageImmunityRemovedEventHandler(ICreatureService creatureService, ILoggingService loggingService)
        {
            _creatureService = creatureService;
            _loggingService = loggingService;
        }

        public async Task HandleAsync(CreatureTemporaryDamageImmunityRemovedEvent domainEvent)
        {
            await _creatureService.RemoveTemporaryDamageImmunityAsync(domainEvent.CreatureId, domainEvent.CreatureName, domainEvent.DamageType);

            string logMessage = $"The creature {domainEvent.CreatureName} (ID: {domainEvent.CreatureId}) has lost the temporary immunity to the following damage type: '{domainEvent.DamageType}'";
            await _loggingService.LogMessageAsync(logMessage);
        }
    }
}
