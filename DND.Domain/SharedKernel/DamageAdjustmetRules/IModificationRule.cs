namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Identify a rule that provides damage modification and extends it
    /// adding methods specific to damage modification rules.
    /// </summary>
    public interface IModificationRule : IDamageAdjustmentRule
    {
        /// <summary>
        /// Provides the modification factor for the given damage type, source,
        /// and whether the weapon is silvered or not.
        /// </summary>
        /// <param name="damageType"></param>
        /// <param name="damageSource"></param>
        /// <param name="isSilvered"></param>
        /// <returns></returns>
        public float GetModificationFactor(DamageType damageType, DamageSource damageSource = DamageSource.Mundane, bool isSilvered = false);
    }
}
