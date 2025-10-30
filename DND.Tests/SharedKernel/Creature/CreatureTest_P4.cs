using DND.Domain.SharedKernel;

namespace DND.Tests.SharedKernel
{
    public partial class CreatureTest
    {
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
    }
}

