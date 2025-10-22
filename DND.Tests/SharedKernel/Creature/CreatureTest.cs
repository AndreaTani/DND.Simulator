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

            sut.SetupImmunity(DamageType.Fire);
            sut.SetupResistance(DamageType.Acid);
            sut.SetupVulnerability(DamageType.Cold);

            return sut;
        }

        [Fact]
        public void CalculateFinalDamageOnLoneFighter_WhenPlainDamage_ReturnBaseDamage()
        {
            // Arrange
            var sut = SetupTargetCreatureLoneFighter();

            int baseDamage = 14;
            var damageType = DamageType.Slashing;
            var damageSource = DamageSource.Mundane;
            bool isSilvered = false;

            // Act
            int finalDamage = sut.CalculateFinalDamage(baseDamage, damageType, damageSource, isSilvered);

            // Assert
            Assert.Equal(baseDamage, finalDamage);
        }
    }
}
