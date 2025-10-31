namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event triggered when a creature condition immunity is Added
    /// </summary>
    /// <param name="CreatureId">The unique identifier of the creature affected.</param>
    /// <param name="Conditions">The specific Conditions whose Immunity was Added (e.g., Condition.Grappled).</param>
    public record CreatureConditionImmunitiesRemovedEvent(
        Guid CreatureId, 
        IEnumerable<Condition> Conditions
        ) : IDomainEvent;
}
