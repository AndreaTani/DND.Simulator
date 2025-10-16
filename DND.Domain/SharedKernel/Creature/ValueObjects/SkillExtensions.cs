namespace DND.Domain.SharedKernel
{
    public static class SkillExtensions
    {
        /// <summary>
        /// Gets the corresponding Ability for a given Skill.
        /// </summary>
        public static Ability GetAbility(this Skill skill)
        {
            return skill switch
            {
                Skill.Acrobatics => Ability.Dexterity,
                Skill.AnimalHandling => Ability.Wisdom,
                Skill.Arcana => Ability.Intelligence,
                Skill.Athletics => Ability.Strength,
                Skill.Deception => Ability.Charisma,
                Skill.History => Ability.Intelligence,
                Skill.Insight => Ability.Wisdom,
                Skill.Intimidation => Ability.Charisma,
                Skill.Investigation => Ability.Intelligence,
                Skill.Medicine => Ability.Wisdom,
                Skill.Nature => Ability.Intelligence,
                Skill.Perception => Ability.Wisdom,
                Skill.Performance => Ability.Charisma,
                Skill.Persuasion => Ability.Charisma,
                Skill.Religion => Ability.Intelligence,
                Skill.SleightOfHand => Ability.Dexterity,
                Skill.Stealth => Ability.Dexterity,
                Skill.Survival => Ability.Wisdom,
                _ => throw new ArgumentOutOfRangeException(nameof(skill), skill, null)
            };
        }
    }
}
