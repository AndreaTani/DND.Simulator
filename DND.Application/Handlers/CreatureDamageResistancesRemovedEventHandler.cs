using DND.Application.Contracts;
using DND.Domain.SharedKernel;

namespace DND.Application.Handlers
{
    public class CreatureDamageResistancesRemovedEventHandler : IDomainEventHandler<CreatureDamageResistancesRemovedEvent>
    {
        private readonly ICreatureService _creatureService;
        private readonly ILoggingService _loggingService;

        public CreatureDamageResistancesRemovedEventHandler(ICreatureService creatureService, ILoggingService loggingService)
        {
            _creatureService = creatureService;
            _loggingService = loggingService;
        }

        public async Task HandleAsync(CreatureDamageResistancesRemovedEvent domainEvent)
        {
            await _creatureService.RemoveDamageResistancesAsync(domainEvent.CreatureId, domainEvent.Type);

            string logMessage = $"The following damage resistances '{string.Join(',', domainEvent.Type)}' has been removed from the creature {domainEvent.Name} (ID: {domainEvent.CreatureId}) for the following reason: '{domainEvent.Reason}'";
            await _loggingService.LogMessageAsync(logMessage);
        }
    }
}
