namespace DND.Domain.SharedKernel.Events
{
    /// <summary>
    /// Domain Event raised when a creature's resistance to a specific damage type is removed.
    /// </summary>
    /// <param name="CreatureId">The unique identifier of the creature affected.</param>
    /// <param name="Type">The specific DamageType whose resistance was removed (e.g., DamageType.Acid).</param>
    /// <param name="Reason">The contextual reason for the removal.</param>
    public record CreatureDamageResistanceRemovedEvent(
            Guid CreatureId,
            DamageType Type,
            RemovalReason Reason
        ) : IDomainEvent;
}
