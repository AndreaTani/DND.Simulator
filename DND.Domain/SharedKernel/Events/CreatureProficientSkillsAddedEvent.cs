namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event triggered when a creature proficient skill is Added
    /// </summary>
    /// <param name="CreatureId">The unique identifier of the affected creature.</param>
    /// <param name="CreatureName">The name of the affected creature.</param>
    /// <param name="Skills">The specific proficient skill Added (e.g., Skill.Athletics).</param>
    public record CreatureProficientSkillsAddedEvent(
        Guid CreatureId,
        string CreatureName,
        IEnumerable<Skill> Skills
        ) : IPermanentAttributeChangedEvent;
}
