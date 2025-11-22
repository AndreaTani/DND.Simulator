namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event triggered when a creature special damage rule is Added, this is the
    /// primary and expected event
    /// </summary>
    /// <param name="CreatureId">The unique identifier of the affected creature.</param>
    /// <param name="CreatureName">Name of the creature</param>
    /// <param name="DamageType">Type of damage related to the rule</param>
    /// <param name="RuleName">The specific special damage rule that was added (e.g., BarbarianResistance).</param>
    public record CreatureSpecialDamageRuleAddedEvent(
        Guid CreatureId,
        string CreatureName,
        DamageType DamageType,
        string RuleName
        ) : IDomainEvent;
}
