namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event triggered when a Temporary Damage Modification is removed
    /// </summary>
    /// <param name="CreatureId">The unique identifier of the creature affected.</param>
    /// <param name="DamageType">The specific DamageType of the temporary effect</param>
    public record CreatureTemporaryDamageModificationRemovedEvent(
        Guid CreatureId,
        string Name,
        DamageType DamageType
        ) : IDomainEvent;
}
