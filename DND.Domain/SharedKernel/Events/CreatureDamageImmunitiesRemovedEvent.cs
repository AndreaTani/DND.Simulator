namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Domain Event raised when a creature's immunity to a specific damage type is removed.
    /// </summary>
    /// <param name="CreatureId">The unique identifier of the affected creature.</param>
    /// <param name="CreatureName">The unique identifier of the affected creature.</param>
    /// <param name="DamageTypes">The specific DamageType whose immunity was removed (e.g., DamageType.Acid).</param>
    /// <param name="Reason">The contextual reason for the removal.</param>
    public record CreatureDamageImmunitiesRemovedEvent(
            Guid CreatureId,
            string CreatureName,
            IEnumerable<DamageType> DamageTypes,
            RemovalReason Reason
        ) : IDomainEvent;

}
