using DND.Domain.SharedKernel.Enum;

namespace DND.Domain.SharedKernel.ValueObjects
{
    public static class SkillTypeExtensions
    {
        /// <summary>
        /// Gets the corresponding AbilityType for a given SkillType.
        /// </summary>
        public static AbilityType GetAbilityType(this SkillType skill)
        {
            return skill switch
            {
                SkillType.Acrobatics => AbilityType.Dexterity,
                SkillType.AnimalHandling => AbilityType.Wisdom,
                SkillType.Arcana => AbilityType.Intelligence,
                SkillType.Athletics => AbilityType.Strength,
                SkillType.Deception => AbilityType.Charisma,
                SkillType.History => AbilityType.Intelligence,
                SkillType.Insight => AbilityType.Wisdom,
                SkillType.Intimidation => AbilityType.Charisma,
                SkillType.Investigation => AbilityType.Intelligence,
                SkillType.Medicine => AbilityType.Wisdom,
                SkillType.Nature => AbilityType.Intelligence,
                SkillType.Perception => AbilityType.Wisdom,
                SkillType.Performance => AbilityType.Charisma,
                SkillType.Persuasion => AbilityType.Charisma,
                SkillType.Religion => AbilityType.Intelligence,
                SkillType.SleightOfHand => AbilityType.Dexterity,
                SkillType.Stealth => AbilityType.Dexterity,
                SkillType.Survival => AbilityType.Wisdom,
                _ => throw new ArgumentOutOfRangeException(nameof(skill), skill, null)
            };
        }
    }
}
