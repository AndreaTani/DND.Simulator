namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Domain Event raised when a creature's resistance to a specific damage type is removed.
    /// </summary>
    /// <param name="CreatureId">The unique identifier of the affected creature.</param>
    /// <param name="CreatureName">The unique identifier of the affected creature.</param>
    /// <param name="DamageType">The specific DamageType whose resistance was removed (e.g., DamageType.Acid).</param>
    /// <param name="Reason">The contextual reason for the removal.</param>
    public record CreatureDamageResistancesRemovedEvent(
        Guid CreatureId,
        string CreatureName,
        IEnumerable<DamageType> DamageType,
        RemovalReason Reason
        ) : IDomainEvent;
}
