namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Implementation of damage adjustment rule for physical damage resistance
    /// when the source of damage is non magical and the weapon is not silvered.
    /// </summary>
    public class PhysicalDamageNonMagicalNonSilveredResistanceRule : IDamageAdjustmentRule //IModificationRule???
    {
        public DamageType TypeOfDamage { get; protected set; }

        public int Apply(int baseDamage, DamageType damageType, DamageSource damageSource, bool isSilvered)
        {
            if((damageType == DamageType.Bludgeoning ||
                 damageType == DamageType.Piercing ||
                 damageType == DamageType.Slashing) &&
                 damageSource == DamageSource.Mundane &&
                 !isSilvered)
            {
                TypeOfDamage = damageType;
                return baseDamage / 2;
            }

            return baseDamage;
        }

        public DamageType GetDamageType()
        {
            return TypeOfDamage;
        }
    }
}
