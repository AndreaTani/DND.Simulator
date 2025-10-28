using DND.Domain.SharedKernel;

namespace DND.Tests.SharedKernel
{
    public partial class CreatureTest
    {
        [Theory]
        [MemberData(nameof(AllDamageTypes))]
        public void CalculateFinalDamage_WhenResistanceCreatesFractionalDamage_MustRoundDown(DamageType damageType)
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

            sut.SetupResistance(damageType);
            int expectedDamage = 7;
            int baseDamage = 15;

            // Act
            int finalDamage = sut.CalculateFinalDamage(baseDamage, damageType, DamageSource.Magical, false);

            // Assert
            Assert.Equal(expectedDamage, finalDamage);
        }

        [Theory]
        [MemberData(nameof(AllConditions))]
        public void Creature_WhenImmuneToConditionAndAddedThatCondition_MustFireImmuynityEvent(Condition condition)
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

            sut.SetupConditionImmunity(condition);
            var initialConditions = sut.Conditions;

            // Act
            sut.SetupCondition(condition);
            var triggeredEvent = sut.DomainEvents.OfType<CreatureImmuneToConditionsEvent>().FirstOrDefault();
            var finaConditions = sut.Conditions;

            // Assert
            Assert.DoesNotContain(condition, finaConditions);
            Assert.Equal(initialConditions, finaConditions);
            Assert.NotNull(triggeredEvent);
            Assert.Contains(condition, triggeredEvent.Conditions);
        }
    }
}
