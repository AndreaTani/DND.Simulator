namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event triggered when a creature sense is Added
    /// </summary>
    /// <param name="CreatureId">The unique identifier of the affected creature.</param>
    /// <param name="CreatureName">The name of the affected creature.</param>
    /// <param name="Senses">The specific Senses Added (e.g., Sense.Darkvision).</param>
    public record CreatureSensesAddedEvent(
        Guid CreatureId,
        string CreatureName,
        IEnumerable<Sense> Senses
        ) : IPermanentAttributeChangedEvent;
}
