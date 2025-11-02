namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event triggered when a creature expert skill is Removed
    /// </summary>
    /// <param name="CreatureId">The unique identifier of the creature affected.</param>
    /// <param name="Skills">The specific expert skill Remopved (e.g., Skill.Athletics).</param>
    public record CreatureExpertSkillsRemovedEvent(
        Guid CreatureId,
        IEnumerable<Skill> Skills
        ) : IDomainEvent;
}
