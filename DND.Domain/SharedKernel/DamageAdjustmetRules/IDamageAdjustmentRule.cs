namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Basic interface for damage adjustment rules, such as resistances, 
    /// immunities, and vulnerabilities. It defines a common method to apply
    /// the adjustment to a given base damage, making possible to implement 
    /// different strategies for damage modification that could be used in 
    /// several situations such as different creature abilities, environmental
    /// effects, or special conditions or creature traits.
    /// All the rules implementing this interface should provide their own 
    /// logic for how the damage is adjusted.
    /// </summary>
    public interface IDamageAdjustmentRule
    {
        /// <summary>
        /// Common method to apply the damage adjustment rule.
        /// </summary>
        /// <param name="baseDamage"></param>
        /// <param name="damageType"></param>
        /// <param name="damageSource"></param>
        /// <param name="isSilvered"></param>
        /// <returns>Total number of Hit points calculated</returns>
        int Apply(int baseDamage, DamageType damageType, DamageSource damageSource, bool isSilvered);
    }
}
