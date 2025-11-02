namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event triggered when a creature proficient skill is Added
    /// </summary>
    /// <param name="CreatureId">The unique identifier of the creature affected.</param>
    /// <param name="Skills">The specific proficient skill Added (e.g., Skill.Athletics).</param>
    public record CreatureProficientSkillsAddedEvent(
        Guid CreatureId,
        IEnumerable<Skill> Skills
        ) : IDomainEvent;
}
