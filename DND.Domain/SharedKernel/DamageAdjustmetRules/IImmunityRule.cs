namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Identify a rule that provides damage immunity and extends it
    /// with methods specific to damage immunity rules.
    /// </summary>
    public interface IImmunityRule : IDamageAdjustmentRule
    {
        /// <summary>
        /// Checks if the damage is immune based on the damage type, source,
        /// and whether the weapon is silvered or not.
        /// </summary>
        public bool IsImmune(DamageType damageType, DamageSource damageSource = DamageSource.Mundane, bool isSilvered = false);
    }
}
