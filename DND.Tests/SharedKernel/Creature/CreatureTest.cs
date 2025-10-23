using DND.Domain.SharedKernel;

namespace DND.Tests.SharedKernel
{
    public class CreatureTest
    {
        private static readonly Dictionary<Ability, int> fighterScores = new Dictionary<Ability, int>
        {
            { Ability.Strength, 18 },
            { Ability.Dexterity, 12 },
            { Ability.Constitution, 16 },
            { Ability.Intelligence, 10 },
            { Ability.Wisdom, 8 },
            { Ability.Charisma, 8 }
        };

        [Theory]
        [InlineData(DamageType.Slashing, 14, 14)]
        [InlineData(DamageType.Fire, 0, 14)]
        [InlineData(DamageType.Acid, 7, 14)]
        [InlineData(DamageType.Cold, 28, 14)]
        public void CalculateFinalDamageOnLoneFighter_WhenDamage_ReturnCorrectDamage(DamageType damageType, int expectedDamage, int baseDamage)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 49,
                speed: new Speed(),
                level: 5
                );

            sut.SetupImmunity(DamageType.Fire);
            sut.SetupResistance(DamageType.Acid);
            sut.SetupVulnerability(DamageType.Cold);

            var damageSource = DamageSource.Mundane;
            bool isSilvered = false;

            // Act
            int finalDamage = sut.CalculateFinalDamage(baseDamage, damageType, damageSource, isSilvered);

            // Assert
            Assert.Equal(expectedDamage, finalDamage);
            Assert.True(sut.IsImmuneTo(DamageType.Fire));
            Assert.True(sut.IsResistantTo(DamageType.Acid));
            Assert.True(sut.IsVulnerableTo(DamageType.Cold));
            Assert.False(sut.IsImmuneTo(DamageType.Acid));
            Assert.False(sut.IsResistantTo(DamageType.Cold));
            Assert.False(sut.IsVulnerableTo(DamageType.Fire));
        }

        [Theory]
        [InlineData(DamageType.Slashing, DamageSource.Mundane, false, 5, 10)]
        [InlineData(DamageType.Bludgeoning, DamageSource.Mundane, false, 5, 10)]
        [InlineData(DamageType.Piercing, DamageSource.Mundane, false, 5, 10)]
        [InlineData(DamageType.Slashing, DamageSource.Magical, false, 10, 10)]
        [InlineData(DamageType.Bludgeoning, DamageSource.Magical, false, 10, 10)]
        [InlineData(DamageType.Piercing, DamageSource.Magical, false, 10, 10)]
        [InlineData(DamageType.Slashing, DamageSource.Natural, false, 5, 10)]
        [InlineData(DamageType.Bludgeoning, DamageSource.Natural, false, 5, 10)]
        [InlineData(DamageType.Piercing, DamageSource.Natural, false, 5, 10)]
        [InlineData(DamageType.Slashing, DamageSource.Mundane, true, 10, 10)]
        [InlineData(DamageType.Bludgeoning, DamageSource.Mundane, true, 10, 10)]
        [InlineData(DamageType.Piercing, DamageSource.Mundane, true, 10, 10)]
        [InlineData(DamageType.Slashing, DamageSource.Magical, true, 10, 10)]
        [InlineData(DamageType.Bludgeoning, DamageSource.Magical, true, 10, 10)]
        [InlineData(DamageType.Piercing, DamageSource.Magical, true, 10, 10)]
        [InlineData(DamageType.Slashing, DamageSource.Natural, true, 10, 10)]
        [InlineData(DamageType.Bludgeoning, DamageSource.Natural, true, 10, 10)]
        [InlineData(DamageType.Piercing, DamageSource.Natural, true, 10, 10)]
        [InlineData(DamageType.Cold, DamageSource.Mundane, false, 10, 10)]
        [InlineData(DamageType.Cold, DamageSource.Natural, false, 10, 10)]
        public void CalculateFinalDamageOnLoneFighter_WhenWerewolfStyleRuleApplies_ReturnCorrectDamage(DamageType damageType, DamageSource damageSource, bool isSilvered, int expectedDamage, int baseDamage)
        {
            // Arrange
            var sut = new SimpleCreature(
                name: "Lone Fighter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 49,
                speed: new Speed(),
                level: 5
                );

            sut.SetupSpecialRule(new PhysicalDamageNonMagicalNonSilveredResistanceRule());

            // Act
            int finalDamage = sut.CalculateFinalDamage(baseDamage, damageType, damageSource, isSilvered);

            // Assert
            Assert.Equal(expectedDamage, finalDamage);
        }
    }
}
