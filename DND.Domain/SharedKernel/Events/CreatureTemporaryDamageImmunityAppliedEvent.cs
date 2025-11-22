namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event triggered when a Temporary immunity is added
    /// </summary>
    /// <param name="CreatureId">The unique identifier of the affected creature.</param>
    /// <param name="SourceId">The unique identifier of the creature that caused the effect.</param>
    /// <param name="Modification">The specific TemporaryImmunityModification added (e.g., Immunity to DamageType.Fire until the end of turn 47 for the creature).</param>
    public record CreatureTemporaryDamageImmunityAppliedEvent(
        Guid CreatureId,
        Guid SourceId,
        TemporaryImmunityModification Modification
        ) : IDomainEvent;
}
