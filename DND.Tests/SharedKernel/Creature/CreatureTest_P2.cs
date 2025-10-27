using DND.Domain.SharedKernel;

namespace DND.Tests.SharedKernel
{
    public partial class CreatureTest
    {
        [Theory]
        [InlineData(7, 15, DamageType.Acid)]
        public void CalculateFinalDamage_WhenResistanceCreatesFractionalDamage_MustRoundDown(int expectedDamage, int baseDamage, DamageType damageType)
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

            // Act
            int finalDamage = sut.CalculateFinalDamage(baseDamage, damageType, DamageSource.Magical, false);

            // Assert
            Assert.Equal(expectedDamage, finalDamage);
        }
    }
}
