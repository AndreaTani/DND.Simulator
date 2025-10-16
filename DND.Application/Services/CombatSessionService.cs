namespace DND.Application.Services
{
    public class CombatSessionService
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
        //    // 1. Caricamento
        //    var creature = await _repository.GetByIdAsync(creatureId);
        //    if (creature == null) return;

        //    // 2. Azione di Dominio (gli eventi sono generati e raccolti)
        //    creature.TakeDamage(damage, type);

        //    // 3. Persistenza: Salva lo stato modificato (DB Transaction)
        //    await _repository.UpdateAsync(creature);

        //    // 4. Dispersione degli Eventi (Event Dispatcher si occupa di chiamare gli Handler)
        //    await _dispatcher.DispatchAsync(creature.DomainEvents);

        //    // 5. Pulizia
        //    creature.ClearDomainEvents();
        //}
    }
}
