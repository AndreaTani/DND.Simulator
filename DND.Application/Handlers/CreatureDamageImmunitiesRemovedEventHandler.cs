using DND.Application.Contracts;
using DND.Domain.SharedKernel;

namespace DND.Application.Handlers
{
    public class CreatureDamageImmunitiesRemovedEventHandler : IDomainEventHandler<CreatureDamageImmunitiesRemovedEvent>
    {
        private readonly ICreatureService _creatureService;
        private readonly ILoggingService _loggingService;

        public CreatureDamageImmunitiesRemovedEventHandler(ICreatureService creatureService, ILoggingService loggingService)
        {
            _creatureService = creatureService;
            _loggingService = loggingService;
        }

        public async Task HandleAsync(CreatureDamageImmunitiesRemovedEvent domainEvent)
        {
            await _creatureService.RemoveDamageImmunitiesAsync(domainEvent.CreatureId, domainEvent.DamageTypes);

            string logMessage = $"The following damage immunities '{string.Join(',', domainEvent.DamageTypes)}' have been removed from Creature {domainEvent.Name} (ID: {domainEvent.CreatureId}) for {domainEvent.Reason.ToString()}";
             await _loggingService.LogMessageAsync(logMessage);
        }
    }
}
