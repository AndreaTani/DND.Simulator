using DND.Application.Contracts;
using DND.Domain.SharedKernel;

namespace DND.Application.Handlers
{
    public  class CreatureStatusHandler : IDomainEventHandler<CreatureHPChangedEvent>
    {
        // TODO: private readonly ICreatureRepository _repository;

        public async Task Handle(CreatureHPChangedEvent domainEvent)
        {
            await Task.CompletedTask;

            // TODO: Implement the logic to update creature status based on HP changes
            // as described in this example

            // Example logic (commented out as repository is not implemented):
            //var creature = await _repository.GetByIdAsync(domainEvent.CreatureId);
            //if (creature == null) return;

            //// 1. Morte Istantanea (HP <= -MaxHP)
            //if (domainEvent.CurrentHitPoints <= -creature.MaxHitPoints)
            //{
            //    creature.ApplyDeath();
            //}
            //// 2. Incoscienza (HP <= 0 ma non morto)
            //else if (domainEvent.CurrentHitPoints <= 0)
            //{
            //    creature.ApplyUnconsciousness();
            //}
            //// 3. Risveglio (Se era inconscio ma ora HP > 0)
            //else if (domainEvent.PreviousHitPoints <= 0 && domainEvent.CurrentHitPoints > 0)
            //{
            //    creature.Revive();
            //}

            //await _repository.UpdateAsync(creature);
        }
    }
}
