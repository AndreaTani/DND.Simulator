namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event triggered when a creature condition immunity is removed
    /// </summary>
    /// <param name="CreatureId">The unique identifier of the affected creature.</param>
    /// <param name="CreatureName">The name of the affected creature.</param>
    /// <param name="Conditions">The specific Conditions whose Immunity was removed (e.g., Condition.Grappled).</param>
    public record CreatureConditionImmunitiesRemovedEvent(
        Guid CreatureId,
        string CreatureName,
        IEnumerable<Condition> Conditions
        ) : IDomainEvent;
}
