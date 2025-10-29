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
        /// Gets the damage type associated with this rule.
        /// </summary>
        /// <returns></returns>
        public DamageType GetDamageType();

        /// <summary>
        /// Identifies a rule by a mnemonic
        /// </summary>
        public string Name { get; }
    }
}
