using DND.Domain.SharedKernel;

namespace DND.Tests.SharedKernel
{
    public partial class CreatureTest
    {
        // Test data for all senses
        public static TheoryData<Sense> AllSenses => [.. Enum.GetValues(typeof(Sense)).Cast<Sense>().ToArray()];

        // Test data for all languages
        public static TheoryData<Language> AllLanguages => [.. Enum.GetValues(typeof(Language)).Cast<Language>().ToArray()];


        [Theory]
        [MemberData(nameof(AllConditions))]
        public void AddConditionImmunities_WhenAddingConditionImmunities_ShouldAddThemWithoutDuplicates(Condition condition)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 49,
                currentHitPoints: 49,
                speed: new Speed(),
                level: 5
                );

            // Act
            sut.SetupConditionImmunities([condition, condition]);
            var conditionImmunities = sut.ConditionImmunities;

            // Assert
            Assert.Contains(condition, conditionImmunities);
            Assert.Equal(1, conditionImmunities.Count(c => c == condition));
        }

        [Theory]
        [MemberData(nameof(AllConditions))]
        public void AddConditionImmunity_WhenAddingConditionImmunity_ShouldAddItWithoutDuplicates(Condition condition)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 49,
                currentHitPoints: 49,
                speed: new Speed(),
                level: 5
                );

            // Act
            sut.SetupConditionImmunity(condition);
            sut.SetupConditionImmunity(condition);
            var conditionImmunities = sut.ConditionImmunities;

            // Assert
            Assert.Contains(condition, conditionImmunities);
            Assert.Equal(1, conditionImmunities.Count(c => c == condition));
        }

        [Theory]
        [MemberData(nameof(AllConditions))]
        public void AddConditions_WhenAddingConditions_ShouldAddItWithoutDuplicates(Condition condition)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 49,
                currentHitPoints: 49,
                speed: new Speed(),
                level: 5
                );

            // Act
            sut.SetupConditions([condition, condition]);
            var conditions = sut.Conditions;

            // Assert
            Assert.Contains(condition, conditions);
            Assert.Equal(1, conditions.Count(c => c == condition));
        }

        [Theory]
        [MemberData(nameof(AllConditions))]
        public void AddCondition_WhenAddingCondition_ShouldAddItWithoutDuplicates(Condition condition)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 49,
                currentHitPoints: 49,
                speed: new Speed(),
                level: 5
                );

            // Act
            sut.SetupCondition(condition);
            sut.SetupCondition(condition);
            var conditions = sut.Conditions;

            // Assert
            Assert.Contains(condition, conditions);
            Assert.Equal(1, conditions.Count(c => c == condition));
        }

        [Theory]
        [MemberData(nameof(AllSenses))]
        public void AddSenses_WhenAddingSenses_ShouldAddThemWithoutDuplicates(Sense sense)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 49,
                currentHitPoints: 49,
                speed: new Speed(),
                level: 5
                );

            // Act
            sut.SetupSenses([sense, sense]);
            var senses = sut.Senses;

            // Assert
            Assert.Contains(sense, senses);
            Assert.Equal(1, senses.Count(s => s == sense));
        }

        [Theory]
        [MemberData(nameof(AllSenses))]
        public void AddSense_WhenAddingSense_ShouldAddItWithoutDuplicates(Sense sense)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 49,
                currentHitPoints: 49,
                speed: new Speed(),
                level: 5
                );

            // Act
            sut.SetupSense(sense);
            sut.SetupSense(sense);
            var senses = sut.Senses;

            // Assert
            Assert.Contains(sense, senses);
            Assert.Equal(1, senses.Count(s => s == sense));
        }

        [Theory]
        [MemberData(nameof(AllLanguages))]
        public void AddLanguages_WhenAddingLanguages_ShouldAddThemWithoutDuplicates(Language language)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 49,
                currentHitPoints: 49,
                speed: new Speed(),
                level: 5
                );

            // Act
            sut.SetupLanguages([language, language]);
            var languages = sut.Languages;

            // Assert
            Assert.Contains(language, languages);
            Assert.Equal(1, languages.Count(l => l == language));
        }

        [Theory]
        [MemberData(nameof(AllLanguages))]
        public void AddLanguage_WhenAddingLanguage_ShouldAddItWithoutDuplicates(Language language)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 49,
                currentHitPoints: 49,
                speed: new Speed(),
                level: 5
                );

            // Act
            sut.SetupLanguage(language);
            sut.SetupLanguage(language);
            var languages = sut.Languages;

            // Assert
            Assert.Contains(language, languages);
            Assert.Equal(1, languages.Count(l => l == language));
        }
    }
}

