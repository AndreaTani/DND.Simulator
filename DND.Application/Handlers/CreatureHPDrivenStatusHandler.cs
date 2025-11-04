using DND.Application.Contracts;
using DND.Domain.SharedKernel;

namespace DND.Application.Handlers
{
    /// <summary>
    /// Handles the CreatureHPChangedEvent to check for critical status changes (Unconscious or Dead).
    /// This handler is responsible for determining the rule-based outcome (the Condition) 
    /// and telling the ICreatureService to apply it.
    /// </summary>
    public class CreatureHPDrivenStatusHandler : IDomainEventHandler<CreatureHPChangedEvent>
    {
        private readonly ICreatureService _creatureService;

        public CreatureHPDrivenStatusHandler(ICreatureService creatureService)
        {
            _creatureService = creatureService;
        }

        /// <summary>
        /// Calculates and applies critical conditions based on the creature's HP according to D&D rules.
        /// If the damage reduces HP to 0 or below, it applies 'Unconscious'.
        /// If the damage reduces HP to a negative number equal to or exceeding Max HP, it applies 'Dead'.
        /// If the HP is above 0, no condition is applied.
        /// </summary>
        /// <param name="domainEvent"></param>
        /// <returns></returns>
        public async Task Handle(CreatureHPChangedEvent domainEvent)
        {
            await _creatureService.HandleCreatureHpStatusAsync(
                domainEvent.CreatureId,
                domainEvent.MaxHp,
                domainEvent.CurrentHp
            );

            Condition conditionToApply = Condition.None;
            int currentHp = domainEvent.CurrentHp;
            int maxHp = domainEvent.MaxHp;

            if (currentHp <= -maxHp)
            {
                conditionToApply = Condition.Dead;
            }
            else if (currentHp <= 0)
            {
                conditionToApply = Condition.Unconscious;
            }

            if (conditionToApply != Condition.None)
            {
                await _creatureService.ApplyConditionsAsync(domainEvent.CreatureId, conditionToApply);
            }
        }
    }
}
