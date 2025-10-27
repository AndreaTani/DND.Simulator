using DND.Domain.SharedKernel;

namespace DND.Tests.SharedKernel
{
    public class SkillExtensionsTest
    {
        [Theory]
        [InlineData(Skill.Acrobatics, Ability.Dexterity)]
        [InlineData(Skill.AnimalHandling, Ability.Wisdom)]
        [InlineData(Skill.Arcana, Ability.Intelligence)]
        [InlineData(Skill.Athletics, Ability.Strength)]
        [InlineData(Skill.Deception, Ability.Charisma)]
        [InlineData(Skill.History, Ability.Intelligence)]
        [InlineData(Skill.Insight, Ability.Wisdom)]
        [InlineData(Skill.Intimidation, Ability.Charisma)]
        [InlineData(Skill.Investigation, Ability.Intelligence)]
        [InlineData(Skill.Medicine, Ability.Wisdom)]
        [InlineData(Skill.Nature, Ability.Intelligence)]
        [InlineData(Skill.Perception, Ability.Wisdom)]
        [InlineData(Skill.Performance, Ability.Charisma)]
        [InlineData(Skill.Persuasion, Ability.Charisma)]
        [InlineData(Skill.Religion, Ability.Intelligence)]
        [InlineData(Skill.SleightOfHand, Ability.Dexterity)]
        [InlineData(Skill.Stealth, Ability.Dexterity)]
        [InlineData(Skill.Survival, Ability.Wisdom)]
        public void GetAbility_WhenCalledWithParameters_ReturnsCorrectMapping(Skill inputSkill, Ability expectedAbility)
        {
            // Act
            var actualAbility = inputSkill.GetAbility();

            // Assert
            Assert.Equal(expectedAbility, actualAbility);

        }

        [Fact]
        public void GetAbility_WhenCalledWithUnexistingOrEmptyParameters_Throws()
        {
            // Arrange
            Skill invalidSkill = (Skill)999;

            // Act
            void act() => invalidSkill.GetAbility();

            // Assert
            Assert.ThrowsAny<ArgumentOutOfRangeException>(act);
        }
    }
}
