namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event fired by the UI/Player when a death save roll is made.
    /// This carries the result of the d20 roll to be processed.
    /// </summary>
    public record CreatureDeathSaveRolledEvent(
        Guid CreatureId,
        string CreatureName,
        int RollValue
    ) : IDomainEvent;
}