namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event triggered when a creature damage vulnerabilities are Added
    /// </summary>
    /// <param name="CreatureId">The unique identifier of the affected creature.</param>
    /// <param name="CreatureName">The name of the affected creature.</param>
    /// <param name="DamageTypes">The specific DamageType whose vulnerabilities was added (e.g., DamageType.Acid).</param>
    public record CreatureDamageVulnerabilitiesAddedEvent(
        Guid CreatureId,
        string CreatureName,
        IEnumerable<DamageType> DamageTypes
        ) : IDomainEvent;
}
