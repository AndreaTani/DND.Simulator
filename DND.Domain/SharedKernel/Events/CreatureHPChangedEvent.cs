using DND.Domain.SharedKernel.Events;

namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event triggered when a creature's hit points (HP) change.
    /// </summary>
    /// <param name="CreatureId">Id of the creature whose HP changed</param>
    /// <param name="PreviousHp">Amount of HP of the creature before the change</param>
    /// <param name="CurrentHp">Amount of HP of the creature after the change</param>
    /// <param name="MaxHp">Maximum amonut of HP of the creature</param>
    /// <param name="Amount">Amount of change in HP; negative values are for damage, positiveare for heal</param>
    /// <param name="Type">Type of Damage sustained, default None for healing</param>
    public record CreatureHPChangedEvent(
        Guid CreatureId, 
        int PreviousHp, 
        int CurrentHp, 
        int MaxHp, 
        int Amount, 
        DamageType Type = DamageType.None
        ) : IDomainEvent;
}
