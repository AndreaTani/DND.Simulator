namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Standard implementation of damage adjustment rule for damage resistance.
    /// </summary>
    public class SimpleDamageResisistanceRule : IModificationRule
    {
        private readonly DamageType _resistanceType;

        public SimpleDamageResisistanceRule(DamageType resistanceType) => _resistanceType = resistanceType;

        public int Apply(int baseDamage, DamageType damageType, DamageSource damageSource, bool isSilvered)
        {
            return damageType == _resistanceType ? baseDamage / 2 : baseDamage;
        }

        public DamageType GetDamageType()
        {
            return _resistanceType;
        }

        public float GetModificationFactor(DamageType damageType, DamageSource damageSource = DamageSource.Mundane, bool isSilvered = false)
        {
            if(damageType == _resistanceType)
                return 0.5f;

            return 1.0f;
        }
    }
}

