namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event triggered when a creature expert skill is Added
    /// </summary>
    /// <param name="CreatureId">The unique identifier of the creature affected.</param>
    /// <param name="Skills">The specific expert skill Added (e.g., Skill.Athletics).</param>
    public record CreatureExpertSkillsAddedEvent(
        Guid CreatureId,
        IEnumerable<Skill> Skills
        ) : IDomainEvent;
}
