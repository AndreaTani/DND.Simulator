using DND.Application.Contracts;
using DND.Domain.SharedKernel;

namespace DND.Application.Handlers
{
    public class RemoveCreatureFromTheInitiativeTrackerHandler : IDomainEventHandler<CreatureDiedEvent>
    {
        private readonly ICombatSessionService _combatSessionService;
        private readonly ILoggingService _loggingService;

        public RemoveCreatureFromTheInitiativeTrackerHandler(ICombatSessionService combatSessionService, ILoggingService loggingService )
        {
            _combatSessionService = combatSessionService;
            _loggingService = loggingService;
        }

        public async Task Handle(CreatureDiedEvent domainEvent)
        {
            string logMessage = $"Creature {domainEvent.CreatureName} (ID: {domainEvent.CreatureId}) has been removed from the initiative tracker.";
            await _combatSessionService.RemoveFromInitiativeAsync(domainEvent.CreatureId);
            await _loggingService.Log(logMessage);
        }
    }
}
