namespace DND.Domain.SharedKernel.Events
{
    /// <summary>
    /// Event triggered when a creature damage immunity is Added, this is the
    /// primary and expected event
    /// </summary>
    /// <param name="CreatureId">The unique identifier of the creature affected.</param>
    /// <param name="Type">The specific DamageType whose resistance was removed (e.g., DamageType.Acid).</param>
    public record CreatureDamageImmunityAddedEvent(
        Guid CreatureId, 
        DamageType Type
        ) : IDomainEvent;
}
