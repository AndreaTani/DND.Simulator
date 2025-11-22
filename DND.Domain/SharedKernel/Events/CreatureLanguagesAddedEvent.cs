namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event triggered when a creature language is Added
    /// </summary>
    /// <param name="CreatureId">The unique identifier of the affected creature.</param>
    /// <param name="CreatureName">The name of the affected creature.</param>
    /// <param name="Languages">The specific language Added (e.g., Language.Goblin).</param>
    public record CreatureLanguagesAddedEvent(
        Guid CreatureId, 
        string CreatureName,
        IEnumerable<Language> Languages
        ) : IPermanentAttributeChangedEvent;
}
