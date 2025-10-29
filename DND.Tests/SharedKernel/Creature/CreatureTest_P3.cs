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

        [Theory]
        [MemberData(nameof(ProficencyBonusTable))]
        public void CreatureProficiencyBonus_ShouldReturn_CorrectValue_BasedOnLevel(int level, int expectedProficiencyBonus)
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
    }
}

