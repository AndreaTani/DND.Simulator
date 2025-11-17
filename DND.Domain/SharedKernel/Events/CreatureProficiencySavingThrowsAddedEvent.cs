namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event triggered when a creature proficiency saving throw ability is Added
    /// </summary>
    /// <param name="CreatureId">The unique identifier of the creature affected.</param>
    /// <param name="Abilities">The specific proficiency saving throw ability skill Added (e.g., Skill.Athletics).</param>
    public record CreatureProficiencySavingThrowsAddedEvent(
        Guid CreatureId,
        string Name, 
        IEnumerable<Ability> Abilities
        ) : IPermanentAttributeChangedEvent;
}
