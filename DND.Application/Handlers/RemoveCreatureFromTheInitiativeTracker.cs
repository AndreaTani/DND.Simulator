using DND.Application.Contracts;
using DND.Domain.SharedKernel;

namespace DND.Application.Handlers
{
    public class RemoveCreatureFromTheInitiativeTracker : IDomainEventHandler<CreatureDiedEvent>
    {
        private readonly ICombatSessionService _combatSessionService;

        public RemoveCreatureFromTheInitiativeTracker(ICombatSessionService combatSessionService)
        {
            _combatSessionService = combatSessionService;
        }

        public Task Handle(CreatureDiedEvent domainEvent)
        {
            _combatSessionService.RemoveFromInitiativeAsync(domainEvent.CreatureId);
            return Task.CompletedTask;
        }
    }
}
