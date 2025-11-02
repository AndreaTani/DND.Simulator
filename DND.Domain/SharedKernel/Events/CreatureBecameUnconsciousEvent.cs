namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event triggered when a creature becomes unconscious
    /// </summary>
    /// <param name="CreatureId">Id of the creature</param>
    /// <param name="Conditions">Always only Unconscious</param>
    public record CreatureBecameUnconsciousEvent(
        Guid CreatureId,
        List<Condition> Conditions
        ) : IDomainEvent
    {
        public CreatureBecameUnconsciousEvent(Guid creatureId)
            : this(creatureId, [Condition.Unconscious])
        {
        }
    }
}
