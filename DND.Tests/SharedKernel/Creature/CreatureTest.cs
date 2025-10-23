using DND.Domain.SharedKernel;

namespace DND.Tests.SharedKernel
{
    public class CreatureTest
    {
        /// <summary>
        /// LoneFighter is a level 5 Orc Fighter under the influence of Asmodeus
        /// which grants him immunity from fire damage and resistance for acid
        /// damage. This comes at a cost: a penalty in the form of vulnerability
        /// to cold damage.
        /// </summary>
        /// <returns></returns>
        private SimpleCreature SetupTargetCreatureLoneFighter()
        {
            Dictionary<Ability, int> fighterScores = new Dictionary<Ability, int>
            {
                { Ability.Strength, 18 },
                { Ability.Dexterity, 12 },
                { Ability.Constitution, 16 },
                { Ability.Intelligence, 10 },
                { Ability.Wisdom, 8 },
                { Ability.Charisma, 8 }
            };

            var sut = new SimpleCreature(
                name: "Lone Fightter",
                creatureType: CreatureType.Humanoid,
                size: Size.Medium,
                abilityScores: new AbilityScores(fighterScores),
                maxHitPoints: 49,
                speed: new Speed(),
                level: 5
                );

            return sut;
        }

        [Theory]
        [InlineData(DamageType.Slashing, 14, 14)]
        [InlineData(DamageType.Fire, 0, 14)]
        [InlineData(DamageType.Acid, 7, 14)]
        [InlineData(DamageType.Cold, 28, 14)]
        public void CalculateFinalDamageOnLoneFighter_WhenDamage_ReturnCalculatedDamage(DamageType damageType, int expectedDamage, int baseDamage)
        {
            // Arrange
            var sut = SetupTargetCreatureLoneFighter();

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
    }
}
