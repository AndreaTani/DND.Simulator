namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event triggered when a creature language is Added
    /// </summary>
    /// <param name="CreatureId">The unique identifier of the creature affected.</param>
    /// <param name="Languages">The specific language Added (e.g., Language.Goblin).</param>
    public record CreatureLanguagesAddedEvent(
        Guid CreatureId, 
        string Name,
        IEnumerable<Language> Languages
        ) : IPermanentAttributeChangedEvent;
}
