using DND.Application.Contracts;
using DND.Domain.SharedKernel;

namespace DND.Application.Handlers
{
    public class RemoveCreatureFromTheInitiativeTrackerHandler : IDomainEventHandler<CreatureDiedEvent>
    {
        private readonly ICombatSessionService _combatSessionService;

        public RemoveCreatureFromTheInitiativeTrackerHandler(ICombatSessionService combatSessionService)
        {
            _combatSessionService = combatSessionService;
        }

        public async Task Handle(CreatureDiedEvent domainEvent)
        {
            await _combatSessionService.RemoveFromInitiativeAsync(domainEvent.CreatureId);
        }
    }
}
