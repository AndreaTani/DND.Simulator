namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event triggered when a creature damage immunity is Added
    /// </summary>
    /// <param name="CreatureId">The unique identifier of the affected creature.</param>
    /// <param name="Name">Name of the affected creature.</param>
    /// <param name="Types">The specific DamageTypes whose immunity was added (e.g., DamageType.Acid).</param>
    public record CreatureDamageImmunitiesAddedEvent(
        Guid CreatureId, 
        string Name,
        IEnumerable<DamageType> Types
        ) : IDomainEvent;
}
