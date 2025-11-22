namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event triggered when a creature becomes unconscious
    /// </summary>
    /// <param name="CreatureId">Id of the creature</param>
    /// <param name="CreatureName">Name of the creature</param>
    /// <param name="Conditions">Always only Unconscious</param>
    public record CreatureBecameUnconsciousEvent(
        Guid CreatureId,
        string CreatureName,
        List<Condition> Conditions
        ) : IDomainEvent
    {
        public CreatureBecameUnconsciousEvent(Guid creatureId, string creatureName)
            : this(creatureId, creatureName, [Condition.Unconscious])
        {
        }
    }
}
