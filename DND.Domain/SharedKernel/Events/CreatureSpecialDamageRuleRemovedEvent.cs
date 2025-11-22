namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event triggered when a creature special damage rule is Removed, this is the
    /// primary and expected event
    /// </summary>
    /// <param name="CreatureId">The unique identifier of the affected creature.</param>
    /// <param name="CreatureName">The name of the affected creature.</param>
    /// <param name="DamageType">The type of damage related to the rule</param>
    /// <param name="RuleName">The specific special damage rule that was removed (e.g., BarbarianResistance).</param>
    public record CreatureSpecialDamageRuleRemovedEvent(
        Guid CreatureId,
        string CreatureName,
        DamageType DamageType,
        string RuleName
        ) : IDomainEvent;
}
