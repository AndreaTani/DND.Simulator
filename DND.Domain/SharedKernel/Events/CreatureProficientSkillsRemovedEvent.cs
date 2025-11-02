namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event triggered when a creature proficient skill is Removed
    /// </summary>
    /// <param name="CreatureId">The unique identifier of the creature affected.</param>
    /// <param name="Skills">The specific proficient skill Remopved (e.g., Skill.Athletics).</param>
    public record CreatureProficientSkillsRemovedEvent(
        Guid CreatureId,
        IEnumerable<Skill> Skills
        ) : IDomainEvent;
}
