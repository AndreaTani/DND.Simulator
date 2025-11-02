namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event triggered when a creature damage immunity is Added
    /// </summary>
    /// <param name="CreatureId">The unique identifier of the creature affected.</param>
    /// <param name="Types">The specific DamageTypes whose immunity was added (e.g., DamageType.Acid).</param>
    public record CreatureDamageImmunitiesAddedEvent(
        Guid CreatureId, 
        IEnumerable<DamageType> Types
        ) : IDomainEvent;
}
