namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event fired by the UI/Player when a death save roll is made.
    /// This carries the result of the d20 roll to be processed.
    /// </summary>
    /// <param name="CreatureId">Unique Identifier of the creature</param>
    /// <param name="CreatureName">Name of the creature</param>
    /// <param name="RollValue">Value of the roll for the DeathSavingThrow</param>
    public record CreatureDeathSaveRolledEvent(
        Guid CreatureId,
        string CreatureName,
        int RollValue
    ) : IDomainEvent;
}