namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Standard implementation of damage adjustment rule for damage immunities.
    /// </summary>
    public class SimpleDamageImmunityRule : IImmunityRule
    {
        private readonly DamageType _immunityType;

        public SimpleDamageImmunityRule(DamageType immunityType) => _immunityType = immunityType;

        public string Name => $"Simple Immunity {_immunityType}";

        public DamageType GetDamageType()
        {
            return _immunityType;
        }

        public bool IsImmune(DamageType damageType, DamageSource damageSource = DamageSource.Mundane, bool isSilvered = false)
        {
            return damageType == _immunityType;
        }
    }
}
