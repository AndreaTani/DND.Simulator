namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event triggered when a creature language is removed
    /// </summary>
    /// <param name="CreatureId">The unique identifier of the creature affected.</param>
    /// <param name="Languages">The specific language removed (e.g., Language.Goblin).</param>
    public record CreatureLanguagesRemovedEvent(
        Guid CreatureId,
        string Name,
        IEnumerable<Language> Languages
        ) : IDomainEvent;
}
