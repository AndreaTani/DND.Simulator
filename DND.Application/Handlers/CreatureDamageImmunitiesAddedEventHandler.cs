using DND.Application.Contracts;
using DND.Domain.SharedKernel;

namespace DND.Application.Handlers
{
    public class CreatureDamageImmunitiesAddedEventHandler : IDomainEventHandler<CreatureDamageImmunitiesAddedEvent>
    {
        private readonly ICreatureService _creatureService;
        private readonly ILoggingService _loggingService;

        public CreatureDamageImmunitiesAddedEventHandler(ICreatureService creatureService, ILoggingService loggingService)
        {
            _creatureService = creatureService;
            _loggingService = loggingService;
        }

        public async Task HandleAsync(CreatureDamageImmunitiesAddedEvent domainEvent)
        {
            await _creatureService.AddDamageImmunityAsync(domainEvent.CreatureId, domainEvent.DamageTypes);

            string logMessage = $"The creature {domainEvent.CreatureName} (ID: {domainEvent.CreatureId}) has new damage immunities: '{string.Join(',', domainEvent.DamageTypes)}'";
            await _loggingService.LogMessageAsync(logMessage);
        }
    }
}
