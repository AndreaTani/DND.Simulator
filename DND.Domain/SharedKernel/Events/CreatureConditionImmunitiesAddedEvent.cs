namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event triggered when a creature condition immunity is Added
    /// </summary>
    /// <param name="CreatureId">The unique identifier of the affected creature.</param>
    /// <param name="CreatureName">The name of the affected creature.</param>
    /// <param name="Conditions">The specific Conditions whose Immunity was Added (e.g., Condition.Grappled).</param>
    public record CreatureConditionImmunitiesAddedEvent(
        Guid CreatureId, 
        string CreatureName,
        IEnumerable<Condition> Conditions
        ) : IDomainEvent;
}
