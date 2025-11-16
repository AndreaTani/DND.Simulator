namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event triggered when a creature sense is Added
    /// </summary>
    /// <param name="CreatureId">The unique identifier of the creature affected.</param>
    /// <param name="Senses">The specific Senses Added (e.g., Sense.Darkvision).</param>
    public record CreatureSensesAddedEvent(
        Guid CreatureId,
        string Name,
        IEnumerable<Sense> Senses
        ) : IDomainEvent;
}
