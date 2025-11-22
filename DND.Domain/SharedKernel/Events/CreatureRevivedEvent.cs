namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event triggered when a creature has been revived
    /// </summary>
    /// <param name="CreatureId">Id of the creature</param>
    /// <param name="CreatureName">Name of the creature</param>
    public record CreatureRevivedEvent(
        Guid CreatureId,
        string CreatureName
        ) : IDomainEvent;
}
