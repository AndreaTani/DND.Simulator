namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event triggered when a creature special damage rule is Removed, this is the
    /// primary and expected event
    /// </summary>
    /// <param name="CreatureId">The unique identifier of the creature affected.</param>
    /// <param name="RuleName">The specific special damage rule that was removed (e.g., BarbarianResistance).</param>
    public record CreatureSpecialDamageRuleRemovedEvent(
        Guid CreatureId,
        string RuleName
        ) : IDomainEvent;
}
