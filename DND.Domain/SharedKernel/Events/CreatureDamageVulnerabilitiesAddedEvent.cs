namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event triggered when a creature damage vulnerabilities are Added
    /// </summary>
    /// <param name="CreatureId">The unique identifier of the creature affected.</param>
    /// <param name="Types">The specific DamageType whose vulnerabilities was added (e.g., DamageType.Acid).</param>
    public record CreatureDamageVulnerabilitiesAddedEvent(
        Guid CreatureId,
        string Name,
        IEnumerable<DamageType> Types
        ) : IDomainEvent;
}
