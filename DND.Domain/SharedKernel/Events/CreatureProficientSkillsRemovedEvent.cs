namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event triggered when a creature proficient skill is Removed
    /// </summary>
    /// <param name="CreatureId">The unique identifier of the affected creature.</param>
    /// <param name="CreatureName">The name of the affected creature.</param>
    /// <param name="Skills">The specific proficient skill Remopved (e.g., Skill.Athletics).</param>
    public record CreatureProficientSkillsRemovedEvent(
        Guid CreatureId,
        string CreatureName,
        IEnumerable<Skill> Skills
        ) : IPermanentAttributeChangedEvent;
}
