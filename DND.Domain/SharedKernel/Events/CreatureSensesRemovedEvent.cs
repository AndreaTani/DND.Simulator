namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event triggered when a creature sense is removed
    /// </summary>
    /// <param name="CreatureId">The unique identifier of the creature affected.</param>
    /// <param name="Senses">The specific senses removed (e.g., Sense.Darkvision).</param>
    public record CreatureSensesRemovedEvent(
        Guid CreatureId, 
        IEnumerable<Sense> Senses
        ) : IDomainEvent;
}
