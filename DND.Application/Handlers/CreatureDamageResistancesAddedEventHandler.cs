using DND.Application.Contracts;
using DND.Domain.SharedKernel;

namespace DND.Application.Handlers
{
    public class CreatureDamageResistancesAddedEventHandler : IDomainEventHandler<CreatureDamageResistancesAddedEvent>
    {
        private readonly ICreatureService _creatureService;
        private readonly ILoggingService _loggingService;

        public CreatureDamageResistancesAddedEventHandler(ICreatureService creatureService, ILoggingService loggingService)
        {
            _creatureService = creatureService;
            _loggingService = loggingService;
        }

        public async Task HandleAsync(CreatureDamageResistancesAddedEvent domainEvent)
        {
            await _creatureService.AddDamageResistancesAsync(domainEvent.CreatureId, domainEvent.DamageTypes);

            string logMessage = $"The creature {domainEvent.CreatureName} (ID: {domainEvent.CreatureId}) has ghained new damage resistances '{string.Join(',', domainEvent.DamageTypes)}'";
            await _loggingService.LogMessageAsync(logMessage);
        }
    }
}
