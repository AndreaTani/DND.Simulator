using DND.Domain.SharedKernel.Events;

namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event triggered when a creature is Dying
    /// </summary>
    /// <param name="CreatureId">Id of the creature</param>
    /// <param name="Conditions">Always only Dying</param>
    public record CreatureIsDyingEvent(
        Guid CreatureId,
        List<Condition> Conditions
        ) : IDomainEvent
    {
        public CreatureIsDyingEvent(Guid creatureId)
            : this(creatureId, [Condition.Dying])
        {
        }

    }
}
