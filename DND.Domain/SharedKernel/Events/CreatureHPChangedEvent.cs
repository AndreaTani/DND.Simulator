namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Event triggered when a creature's hit points (HP) change.
    /// </summary>
    public sealed class CreatureHPChangedEvent : IDomainEvent
    {
        public Guid CreatureId { get; }
        public int PreviousHitPoints { get; }
        public int CurrentHitPoints { get; }
        public int MaxHitPoints { get; }

        // Damage amount and damage type are useful for logs, condition changes and reactions tied to specific damage types
        public int DamageAmount { get; }
        public DamageType DamageType { get; }

        public CreatureHPChangedEvent(Guid creatureId, int previousHp, int currentHp, int maxHp, int amount, DamageType damageType = DamageType.None)
        {
            CreatureId = Guid.Empty;
            PreviousHitPoints = previousHp;
            CurrentHitPoints = currentHp;
            MaxHitPoints = maxHp;
            DamageAmount = amount;
            DamageType = damageType;
        }
    }
}
