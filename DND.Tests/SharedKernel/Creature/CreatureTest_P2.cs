using DND.Domain.SharedKernel;

namespace DND.Tests.SharedKernel
{
    public partial class CreatureTest
    {
        // Test data for all conditions
        public static TheoryData<Condition> AllConditions => [.. Enum.GetValues(typeof(Condition)).Cast<Condition>().ToArray()];


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

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Creature_WhenApplyingUnconsciousness_AppliesUnconscousProneConditionsDropConcentrationAndTriggerEvents_DyingEventIfApplyUnconsciousnessWithIsDyingTrue(bool isDying)
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
            sut.ApplyUnconsciousness(isDying);
            var finalConditions = sut.Conditions;
            bool isconcentrating = sut.IsConcentrating;
            var unconsciousEvent = sut.DomainEvents.OfType<CreatureBecameUnconsciousEvent>().FirstOrDefault();
            var isDyingEvent = sut.DomainEvents.OfType<CreatureIsDyingEvent>().FirstOrDefault();
            var proneConditionEvent = sut.DomainEvents.OfType<CreatureAddConditionEvent>()
                .FirstOrDefault(e => e.Conditions.Contains(Condition.Prone));

            // Assert
            Assert.Contains(Condition.Unconscious, finalConditions);
            Assert.Contains(Condition.Prone, finalConditions);
            Assert.False(isconcentrating);
            Assert.NotNull(unconsciousEvent);
            Assert.NotNull(proneConditionEvent);

            if (isDying)
            {
                Assert.Contains(Condition.Dying, finalConditions);
                Assert.NotNull(isDyingEvent);
            }
            else
            {
                Assert.DoesNotContain(Condition.Dying, finalConditions);
                Assert.Null(isDyingEvent);
            }

        }

        [Fact]
        public void Creature_WhenApplyingDead_ApplyDeadRemoveUnconsciousAndDyingDropConcentrationAndTriggerEvents()
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
            sut.ApplyDeath();
            var finalConditions = sut.Conditions;
            bool isconcentrating = sut.IsConcentrating;
            var deadEvent = sut.DomainEvents.OfType<CreatureDiedEvent>().FirstOrDefault();
            var unconsciousEvent = sut.DomainEvents.OfType<CreatureRemoveConditionEvent>()
                .FirstOrDefault(e => e.Conditions.Contains(Condition.Unconscious));
            var dyingEvent = sut.DomainEvents.OfType<CreatureRemoveConditionEvent>()
                .FirstOrDefault(e => e.Conditions.Contains(Condition.Dying));

            // Assert
            Assert.Contains(Condition.Dead, finalConditions);
            Assert.DoesNotContain(Condition.Unconscious, finalConditions);
            Assert.DoesNotContain(Condition.Dying, finalConditions);
            Assert.False(isconcentrating);
            Assert.NotNull(deadEvent);
            Assert.NotNull(unconsciousEvent);
            Assert.NotNull(dyingEvent);
        }
    }
}
