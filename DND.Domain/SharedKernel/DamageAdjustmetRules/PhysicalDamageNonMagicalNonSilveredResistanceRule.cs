namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Implementation of damage adjustment rule for physical damage resistance
    /// when the source of damage is non magical and the weapon is not silvered.
    /// </summary>
    public class PhysicalDamageNonMagicalNonSilveredResistanceRule : IModificationRule, ISpecialDamageAdjustmentRule
    {
        public DamageType TypeOfDamage { get; protected set; }

        public string Name => "Werewolf-style Resistance";

        public DamageType GetDamageType()
        {
            return TypeOfDamage;
        }

        public float GetModificationFactor(DamageType damageType, DamageSource damageSource = DamageSource.Mundane, bool isSilvered = false)
        {
            if ((damageType == DamageType.Bludgeoning ||
                 damageType == DamageType.Piercing ||
                 damageType == DamageType.Slashing) &&
                 damageSource != DamageSource.Magical &&
                 !isSilvered)
            {
                TypeOfDamage = damageType;
                return 0.5f;
            }

            return 1.0f;
        }
    }
}
