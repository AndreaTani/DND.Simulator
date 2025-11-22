namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event triggered when a creature damage resistance is Added, this is the
    /// primary and expected event
    /// </summary>
    /// <param name="CreatureId">The unique identifier of the affected creature.</param>
    /// <param name="CreatureName">The name of the affected creature.</param>
    /// <param name="DamageTypes">The specific DamageType whose resistance was removed (e.g., DamageType.Acid).</param>
    public record CreatureDamageResistancesAddedEvent(
        Guid CreatureId, 
        string CreatureName,
        IEnumerable<DamageType> DamageTypes
        ) : IDomainEvent;
}
