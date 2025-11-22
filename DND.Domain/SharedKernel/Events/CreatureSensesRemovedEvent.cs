namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event triggered when a creature sense is removed
    /// </summary>
    /// <param name="CreatureId">The unique identifier of the affected creature.</param>
    /// <param name="CreatureName">Name of the creature</param>
    /// <param name="Senses">The specific senses removed (e.g., Sense.Darkvision).</param>
    public record CreatureSensesRemovedEvent(
        Guid CreatureId, 
        string CreatureName,
        IEnumerable<Sense> Senses
        ) : IPermanentAttributeChangedEvent;
}
