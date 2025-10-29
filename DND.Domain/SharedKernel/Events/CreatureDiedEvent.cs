using DND.Domain.SharedKernel.Events;

namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event triggered when a creature Dies
    /// </summary>
    /// <param name="CreatureId">Id of the creature</param>
    /// <param name="Conditions">Always only Dead</param>
    public record CreatureDiedEvent(
        Guid CreatureId,
        List<Condition> Conditions
        ) : IDomainEvent
    {
        public CreatureDiedEvent(Guid creatureId)
            : this(creatureId, [Condition.Dead])
        {
        }

    }
}
