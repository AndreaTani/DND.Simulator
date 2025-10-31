using DND.Domain.SharedKernel;

namespace DND.Tests.SharedKernel
{
    public partial class CreatureTest
    {
        // Test data for all conditions
        public static TheoryData<Condition> AllConditions => [.. Enum.GetValues(typeof(Condition)).Cast<Condition>().ToArray()];

        // Test data for vulnerability to all damage type calculations
        public static TheoryData<DamageType, int, int> VulnerabilityDamageTypeExpectedAndBaseDamage
        {
            get
            {
                var data = new TheoryData<DamageType, int, int>();
                foreach (var dt in Enum.GetValues(typeof(DamageType)).Cast<DamageType>())
                {
                    data.Add(dt, 24, 12);
                }
                return data;
            }
        }

        // Test data for resistance to all damage type calculations
        public static TheoryData<DamageType, int, int> ResistanceDamageTypeExpectedAndBaseDamage
        {
            get
            {
                var data = new TheoryData<DamageType, int, int>();
                foreach (var dt in Enum.GetValues(typeof(DamageType)).Cast<DamageType>())
                {
                    data.Add(dt, 6, 12);
                }
                return data;
            }
        }


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

            sut.SetupDamageResistance(damageType);
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

        [Theory]
        [MemberData(nameof(VulnerabilityDamageTypeExpectedAndBaseDamage))]
        public void CalculateFinalDamage_WhenTemporaryVulnerability_CorrectDamageIsComputed(DamageType damageType, int expectedDamage, int baseDamage)
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

            var temporaryVulnerability = new TemporaryDamageModification(damageType, 2.0f, new Guid(), 47, ExpirationType.AtTheEnd);
            sut.ApplyTemporaryDamageModification(temporaryVulnerability);
            var initialIsVulnerable = sut.IsVulnerableTo(damageType);

            // Act
            var damageTaken = sut.CalculateFinalDamage(baseDamage, damageType, DamageSource.Magical, false);
            var finalIsVulnerable = sut.IsVulnerableTo(damageType);

            // Assert
            Assert.True(initialIsVulnerable);
            Assert.True(finalIsVulnerable);
            Assert.Equal(expectedDamage, damageTaken);
        }

        [Theory]
        [MemberData(nameof(ResistanceDamageTypeExpectedAndBaseDamage))]
        public void CalculateFinalDamage_WhenTemporaryResistance_CorrectDamageIsComputed(DamageType damageType, int expectedDamage, int baseDamage)
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

            var temporaryResistance = new TemporaryDamageModification(damageType, 0.5f, new Guid(), 47, ExpirationType.AtTheEnd);
            sut.ApplyTemporaryDamageModification(temporaryResistance);
            var initialIsResistant = sut.IsResistantTo(damageType);

            // Act
            var damageTaken = sut.CalculateFinalDamage(baseDamage, damageType, DamageSource.Magical, false);
            var finalIsResistant = sut.IsResistantTo(damageType);

            // Assert
            Assert.True(initialIsResistant);
            Assert.True(finalIsResistant);
            Assert.Equal(expectedDamage, damageTaken);
        }

        [Theory]
        [MemberData(nameof(AllDamageTypes))]
        public void CalculateFinalDamage_WhenTemporaryImmunty_DamageIsZero(DamageType damageType)
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

            var temporaryImmunity = new TemporaryImmunityModification(damageType, new Guid(), 47, ExpirationType.AtTheEnd);
            sut.ApplyTemporaryDamageImmunity(temporaryImmunity);
            var initialIsImmune = sut.IsImmuneTo(damageType);

            // Act
            var damageTaken = sut.CalculateFinalDamage(50, damageType, DamageSource.Magical, false);
            var finalIsImmune = sut.IsImmuneTo(damageType);

            // Assert
            Assert.True(initialIsImmune);
            Assert.True(finalIsImmune);
            Assert.Equal(0, damageTaken);
        }

        [Theory]
        [MemberData(nameof(AllDamageTypes))]
        public void CalculateFinalDamage_WhenPemanentlyVulnerableAndAddTemporaryResistance_DamageMustBeResistance(DamageType damageType)
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

            sut.SetupDamageVulnerability(damageType);
            int baseDamage = 20;
            int expectedDamage = 10;

            // Act
            var temporaryDamageResistance = new TemporaryDamageModification(damageType, 0.5f, new Guid(), 47, ExpirationType.AtTheBeginning);
            sut.ApplyTemporaryDamageModification(temporaryDamageResistance);
            var damageTaken = sut.CalculateFinalDamage(baseDamage, damageType, DamageSource.Magical, false);

            // Assert
            Assert.Equal(expectedDamage, damageTaken);
        }

        [Theory]
        [MemberData(nameof(AllDamageTypes))]
        public void CalculateFinalDamage_WhenPemanentlyResistantAndAddTemporaryVulnerable_DamageMustBeVulnerable(DamageType damageType)
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

            sut.SetupDamageResistance(damageType);
            int baseDamage = 20;
            int expectedDamage = 40;

            // Act
            var temporaryDamageVulnerability = new TemporaryDamageModification(damageType, 2.0f, new Guid(), 47, ExpirationType.AtTheBeginning);
            sut.ApplyTemporaryDamageModification(temporaryDamageVulnerability);
            var damageTaken = sut.CalculateFinalDamage(baseDamage, damageType, DamageSource.Magical, false);

            // Assert
            Assert.Equal(expectedDamage, damageTaken);
        }

        [Theory]
        [MemberData(nameof(AllDamageTypes))]
        public void CalculateFinalDamage_WhenTemporaryDamageAdded_TemporarydamageSuccessfullyRemovedAndAllCalculationsCorrect(DamageType damageType)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 60,
                currentHitPoints: 60,
                speed: new Speed(),
                level: 5
                );

            var TemporaryDamageMoidicficationId = Guid.NewGuid();
            var temporaryDamageResistance = new TemporaryDamageModification(damageType, 0.5f, TemporaryDamageMoidicficationId, 47, ExpirationType.AtTheBeginning);
            sut.ApplyTemporaryDamageModification(temporaryDamageResistance);
            int baseDamage = 30;

            // Act
            var firstDamageTaken = sut.CalculateFinalDamage(baseDamage, damageType, DamageSource.Magical, false);
            sut.RemoveTempoDamageModification(TemporaryDamageMoidicficationId, damageType);
            var secondDamageTaken = sut.CalculateFinalDamage(baseDamage, damageType, DamageSource.Magical, false);

            // Assert
            Assert.Equal(baseDamage / 2, firstDamageTaken);
            Assert.Equal(baseDamage, secondDamageTaken);
        }

        [Theory]
        [MemberData(nameof(AllDamageTypes))]
        public void CalculateFinalDamage_WhenTemporaryImmunityAdded_TemporaryImmunitySuccessfullyRemovedAndAllCalculationsCorrect(DamageType damageType)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 60,
                currentHitPoints: 60,
                speed: new Speed(),
                level: 5
                );

            var TemporaryImmunityModificationId = Guid.NewGuid();
            var temporaryImmunity = new TemporaryImmunityModification(damageType, TemporaryImmunityModificationId, 47, ExpirationType.AtTheBeginning);
            sut.ApplyTemporaryDamageImmunity(temporaryImmunity);
            int baseDamage = 30;
            
            // Act
            var firstDamageTaken = sut.CalculateFinalDamage(baseDamage, damageType, DamageSource.Magical, false);
            sut.RemoveTempoDamageImmunity(TemporaryImmunityModificationId, damageType);
            var secondDamageTaken = sut.CalculateFinalDamage(baseDamage, damageType, DamageSource.Magical, false);
            
            // Assert
            Assert.Equal(0, firstDamageTaken);
            Assert.Equal(baseDamage, secondDamageTaken);
        }
    }
}

