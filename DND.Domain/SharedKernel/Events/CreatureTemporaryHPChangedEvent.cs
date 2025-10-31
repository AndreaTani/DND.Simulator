namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event triggered when a creature's temporary hit points (HP) change.
    /// </summary>
    /// <param name="CreatureId">Id of the creature whose Temp HP changed</param>
    /// <param name="CurrentTemporaryHp">Current amount of Temp HP after the change</param>
    /// <param name="Amount">The amount of Temp HP changed</param>
    public record CreatureTemporaryHPChangedEvent(
        Guid CreatureId,
        int CurrentTemporaryHp,
        int Amount
        ) : IDomainEvent;
}