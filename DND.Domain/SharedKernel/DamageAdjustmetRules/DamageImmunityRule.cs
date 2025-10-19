namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Standard implementation of damage adjustment rule for damage immunities.
    /// </summary>
    public class DamageImmunityRule : IDamageAdjustmentRule
    {
        private readonly DamageType _immunityType;

        public DamageImmunityRule(DamageType immunityType) => _immunityType = immunityType;

        public int Apply(int baseDamage, DamageType damageType, DamageSource damageSource, bool isSilvered)
        {
            return damageType == _immunityType ? 0 : baseDamage;
        }
    }
}
