using DND.Application.Contracts;
using DND.Domain.SharedKernel;

namespace DND.Application.Handlers
{
    public class CreatureTemporaryDamageImmunityAppliedEventHandler : IDomainEventHandler<CreatureTemporaryDamageImmunityAppliedEvent>
    {
        private readonly ICreatureService _creatureService;
        private readonly ILoggingService _loggingService;

        public CreatureTemporaryDamageImmunityAppliedEventHandler(ICreatureService creatureService, ILoggingService loggingService)
        {
            _creatureService = creatureService;
            _loggingService = loggingService;
        }

        public async Task HandleAsync(CreatureTemporaryDamageImmunityAppliedEvent domainEvent)
        {
            await _creatureService.AddTempopraryDamageImmunityAsync(domainEvent.CreatureId, domainEvent.SourceId, domainEvent.Modification);

            string logMessage = $"The creature {domainEvent.Modification.CreatureName} (ID: {domainEvent.CreatureId}) has gained temporary immunity from The following damage type {domainEvent.Modification.TypeOfDamage} due to creature id {domainEvent.SourceId}";
            await _loggingService.LogMessageAsync(logMessage);
        }
    }
}
