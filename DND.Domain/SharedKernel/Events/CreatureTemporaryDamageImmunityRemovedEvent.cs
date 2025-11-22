namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event triggered when a Temporary Damage Immunity is removed
    /// </summary>
    /// <param name="CreatureId">The unique identifier of the affected creature.</param>
    /// <param name="CreatureName">The name of the creature that caused the effect.</param>
    /// <param name="DamageType">The specific DamageType of the temporary effect</param>
    public record CreatureTemporaryDamageImmunityRemovedEvent(
        Guid CreatureId,
        string CreatureName,
        DamageType DamageType
        ) : IDomainEvent;
}
