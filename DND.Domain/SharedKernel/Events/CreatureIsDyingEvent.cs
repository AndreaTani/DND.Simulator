namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event triggered when a creature is Dying
    /// </summary>
    /// <param name="CreatureId">Id of the creature</param>
    /// <param name="Conditions">Always only Dying</param>
    public record CreatureIsDyingEvent(
        Guid CreatureId,
        string CreatureName,
        List<Condition> Conditions
        ) : IDomainEvent
    {
        public CreatureIsDyingEvent(Guid creatureId, string creatureName)
            : this(creatureId, creatureName, [Condition.Dying])
        {
        }

    }
}
