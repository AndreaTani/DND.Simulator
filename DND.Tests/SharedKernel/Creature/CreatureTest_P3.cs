using DND.Domain.SharedKernel;

namespace DND.Tests.SharedKernel
{
    public partial class CreatureTest
    {
        // Test data for level and proficiency bonus calculations
        public static TheoryData<int, int> ProficencyBonusTable
        {
            get
            {
                var data = new TheoryData<int, int>
                {
                    { 1, 2 },
                    { 2, 2 },
                    { 3, 2 },
                    { 4, 2 },
                    { 5, 3 },
                    { 6, 3 },
                    { 7, 3 },
                    { 8, 3 },
                    { 9, 4 },
                    { 10, 4 },
                    { 11, 4 },
                    { 12, 4 },
                    { 13, 5 },
                    { 14, 5 },
                    { 15, 5 },
                    { 16, 5 },
                    { 17, 6 },
                    { 18, 6 },
                    { 19, 6 },
                    { 20, 6 }
                };
                return data;
            }
        }

        // Test data for all abilities
        public static TheoryData<Ability> AllAbilities => [.. Enum.GetValues(typeof(Ability)).Cast<Ability>().ToArray()];

        // Test data for all skills
        public static TheoryData<Skill> AllSkills => [.. Enum.GetValues(typeof(Skill)).Cast<Skill>().ToArray()];


        [Theory]
        [MemberData(nameof(ProficencyBonusTable))]
        public void CreatureProficiencyBonus_WhenProficiencyBonus_ShouldReturnCorrectValueBasedOnLevel(int level, int expectedProficiencyBonus)
        {
            // Arrange
            var creature = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 50,
                currentHitPoints: 50,
                speed: new Speed(30),
                level: level
            );

            // Act
            var proficiencyBonus = creature.ProficiencyBonus;

            // Assert
            Assert.Equal(expectedProficiencyBonus, proficiencyBonus);
        }

        [Theory]
        [MemberData(nameof(AllAbilities))]
        public void GetSavingThrowModifier_WhenAbilityProficient_ShouldReturnAbilityModifierAddedToProficiencyBonus(Ability ability)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 50,
                currentHitPoints: 50,
                speed: new Speed(30),
                level: 5 // Level 5 gives a proficiency bonus of +3
            );

            int expectedProficiencyBonus = 3; // Level 5 proficiency bonus

            sut.SetupProficientSavingThrow(ability);
            int abilityModifier = sut.AbilityScores.GetModifier(ability);
            int proficiencyBonus = sut.ProficiencyBonus;
            int expectedModifier = abilityModifier + proficiencyBonus;

            // Act
            int savingThrowModifier = sut.GetSavingThrowModifier(ability);

            // Assert
            Assert.Equal(expectedProficiencyBonus, proficiencyBonus);
            Assert.Equal(expectedModifier, savingThrowModifier);
        }

        [Theory]
        [MemberData(nameof(AllAbilities))]
        public void GetSavingThrowModifier_WhenAbilityNotProficient_ShouldReturnOnlyAbilityModifier(Ability ability)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 50,
                currentHitPoints: 50,
                speed: new Speed(30),
                level: 5 // Level 5 gives a proficiency bonus of +3
            );

            int abilityModifier = sut.AbilityScores.GetModifier(ability);
            int expectedModifier = abilityModifier;

            // Act
            int savingThrowModifier = sut.GetSavingThrowModifier(ability);

            // Assert
            Assert.Equal(expectedModifier, savingThrowModifier);
        }

        [Theory]
        [MemberData(nameof(AllSkills))]
        public void GetSkillModifier_WhenSkillProficient_ShouldReturnAbilityModifierAddedToProficiencyBonus(Skill skill)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 50,
                currentHitPoints: 50,
                speed: new Speed(30),
                level: 5 // Level 5 gives a proficiency bonus of +3
            );

            int expectedProficiencyBonus = 3; // Level 5 proficiency bonus
            sut.SetupProficientSkill(skill);
            Ability associatedAbility = SkillExtensions.GetAbility(skill);
            int abilityModifier = sut.AbilityScores.GetModifier(associatedAbility);
            int proficiencyBonus = sut.ProficiencyBonus;
            int expectedModifier = abilityModifier + proficiencyBonus;
            bool isProficient = sut.IsProficientInSkill(skill);
            bool isExpert = sut.HasExpertiseInSkill(skill);

            // Act
            int skillModifier = sut.GetSkillCheckModifier(skill);

            // Assert
            Assert.Equal(expectedProficiencyBonus, proficiencyBonus);
            Assert.Equal(expectedModifier, skillModifier);
            Assert.True(isProficient);
            Assert.False(isExpert);
        }

        [Theory]
        [MemberData(nameof(AllSkills))]
        public void GetSkillModifier_WhenSkillNotProficient_ShouldReturnOnlyAbilityModifier(Skill skill)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 50,
                currentHitPoints: 50,
                speed: new Speed(30),
                level: 5 // Level 5 gives a proficiency bonus of +3
            );

            Ability associatedAbility = SkillExtensions.GetAbility(skill);
            int abilityModifier = sut.AbilityScores.GetModifier(associatedAbility);
            int expectedModifier = abilityModifier;
            bool isProficient = sut.IsProficientInSkill(skill);
            bool isExpert = sut.HasExpertiseInSkill(skill);

            // Act
            int skillModifier = sut.GetSkillCheckModifier(skill);

            // Assert
            Assert.Equal(expectedModifier, skillModifier);
            Assert.False(isProficient);
            Assert.False(isExpert);

        }

        [Theory]
        [MemberData(nameof(AllSkills))]
        public void GetSkillModifier_WhenSkillExpert_ShouldReturnAbilityModifierAddedToDoubleProficiencyBonus(Skill skill)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 50,
                currentHitPoints: 50,
                speed: new Speed(30),
                level: 5 // Level 5 gives a proficiency bonus of +3
            );

            int expectedProficiencyBonus = 3; // Level 5 proficiency bonus
            sut.SetupExpertSkill(skill);
            Ability associatedAbility = SkillExtensions.GetAbility(skill);
            int abilityModifier = sut.AbilityScores.GetModifier(associatedAbility);
            int proficiencyBonus = sut.ProficiencyBonus * 2; // Double proficiency for expertise
            int expectedModifier = abilityModifier + proficiencyBonus;
            bool isProficient = sut.IsProficientInSkill(skill);
            bool isExpert = sut.HasExpertiseInSkill(skill);


            // Act
            int skillModifier = sut.GetSkillCheckModifier(skill);

            // Assert
            Assert.Equal(expectedProficiencyBonus * 2, proficiencyBonus);
            Assert.Equal(expectedModifier, skillModifier);
            Assert.False(isProficient);
            Assert.True(isExpert);
        }

        [Fact]
        public void CreatureCurrentArmorClass_WhenArmorClassIsZero_ShouldReturn10PlusDexterityModifier()
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores), // Dexterity score of 12 (+1 modifier)
                maxHitPoints: 40,
                currentHitPoints: 40,
                speed: new Speed(30),
                armorClass: 0 // Base armor class is set to 0
            );

            int expectedArmorClass = 10 + sut.AbilityScores.GetModifier(Ability.Dexterity); // 10 + 1 = 11

            // Act
            int actualArmorClass = sut.CurrentArmorClass;

            // Assert
            Assert.Equal(expectedArmorClass, actualArmorClass);
        }

        [Fact]
        public void CreatureCurrentArmorClass_WhenArmorClassIsSet_ShouldReturnSetValue()
        {
            // Arrange
            int setArmorClass = 16;
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 40,
                currentHitPoints: 40,
                speed: new Speed(30),
                armorClass: setArmorClass // Base armor class is set to 16
            );

            // Act
            int actualArmorClass = sut.CurrentArmorClass;

            // Assert
            Assert.Equal(setArmorClass, actualArmorClass);
        }
    }
}

