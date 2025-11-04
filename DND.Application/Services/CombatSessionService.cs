using DND.Application.Contracts;

namespace DND.Application.Services
{
    public class CombatSessionService : ICombatSessionService
    {
        /// <summary>
        /// Simplified example of a Service in the Application Layer that coordinates a use case involving domain entities and events.
        /// Commented out to avoid errors due to missing dependencies.
        /// </summary>
        /// 
        //private readonly ICharacterRepository _repository;
        //private readonly IEventDispatcher _dispatcher; // Event Dispatcher (Infrastructure)

        //public async Task ApplyDamageCommand(Guid creatureId, int damage, DamageType type)
        //{
        //    // 1. Loading Aggregate Root
        //    var creature = await _repository.GetByIdAsync(creatureId);
        //    if (creature == null) return;

        //    // 2. Domain Logic Execution
        //    creature.TakeDamage(damage, type);

        //    // 3. Persistence
        //    await _repository.UpdateAsync(creature);

        //    // 4. Event Dispatching
        //    await _dispatcher.DispatchAsync(creature.DomainEvents);

        //    // 5. Clearing Events
        //    creature.ClearDomainEvents();
        //}
        public Task RemoveFromInitiativeAsync(Guid creatureId)
        {
            throw new NotImplementedException();
        }
    }
}
