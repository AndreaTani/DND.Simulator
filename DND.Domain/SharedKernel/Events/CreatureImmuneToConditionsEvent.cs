using DND.Domain.SharedKernel.Events;

namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event triggered when trying to add a condition to the creature but it's immune
    /// </summary>
    /// <param name="CreatureId">Id of the creature</param>
    /// <param name="Conditions">Condition of which the creature is immune</param>
    public record CreatureImmuneToConditionsEvent(
        Guid CreatureId, 
        List<Condition> Conditions
        ) : IDomainEvent;
}
