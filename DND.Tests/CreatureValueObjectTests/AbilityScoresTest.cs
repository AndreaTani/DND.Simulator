using DND.Domain.SharedKernel;

namespace DND.Tests.CreatureValueObjectTests
{
    public class AbilityScoresTest
    {
        // Setup
        private readonly Dictionary<Ability, int> abilityScores = new Dictionary<Ability, int> {
            { Ability.Strength, 18 },
            { Ability.Dexterity, 12 },
            { Ability.Constitution, 16 },
            { Ability.Intelligence, 8 },
            { Ability.Wisdom, 8 },
            { Ability.Charisma, 12 }
        };

        private readonly Dictionary<Ability, int> limitedAbilityScores = new Dictionary<Ability, int> {
            { Ability.Strength, 18 },
            { Ability.Constitution, 16 },
            { Ability.Charisma, 12 }
        };


        [Theory]
        [InlineData(Ability.Strength, 4)]
        [InlineData(Ability.Dexterity, 1)]
        [InlineData(Ability.Constitution, 3)]
        [InlineData(Ability.Intelligence, -1)]
        [InlineData(Ability.Wisdom, -1)]
        [InlineData(Ability.Charisma, 1)]
        public void GetModifier_WhenPassingAbility_ShouldReturnCorrectModifier(Ability ability, int expectedBonus)
        {
            // Arrange
            var sut = new AbilityScores(abilityScores);

            // Act
            var bonus = sut.GetModifier(ability);

            // Assert
            Assert.Equal(expectedBonus, bonus);
        }

        [Fact]
        public void UpdateScore_WhenPassingAnInRangeScore_ShouldReturnTrue()
        {
            // Arrange
            var sut = new AbilityScores(abilityScores);

            // Act
            bool success = sut.UpdateScore(Ability.Intelligence, 20);

            // Assert
            Assert.True(success);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(21)]
        [InlineData(-7)]
        [InlineData(50)]
        public void UpdateScore_WhenPassingAnOutOfRangeScore_ShouldThrow(int outOfRangeScore)
        {
            // Arange
            var sut = new AbilityScores(abilityScores);

            // Act
            void act() => sut.UpdateScore(Ability.Dexterity, outOfRangeScore);

            // Assert
            Assert.ThrowsAny<ArgumentOutOfRangeException>(act);
        }

        [Fact]
        public void AbilityScores_WithDefaultConstructor_ReturnsDefaultAndBonusIsZero()
        {
            // Arrange, Act
            var sut = new AbilityScores();

            // Assert
            foreach(var ability in sut.Scores)
            {
                Assert.Equal(10, ability.Value);
            }
            Assert.Equal(0, sut.GetModifier(Ability.Strength));
        }

        [Fact]
        public void AbilityScore_WithParametrizedConstructor_ReturnsCorrectScoresAndBonusesAndDefaultForAbilitiesNotPassed()
        {
            // Arrange
            var sut = new AbilityScores(limitedAbilityScores);

            // Act & Assert
            //Default Values
            Assert.Equal(10, sut.Scores[Ability.Dexterity]);
            Assert.Equal(0, sut.GetModifier(Ability.Dexterity));

            Assert.Equal(10, sut.Scores[Ability.Intelligence]);
            Assert.Equal(0, sut.GetModifier(Ability.Intelligence));

            Assert.Equal(10, sut.Scores[Ability.Wisdom]);
            Assert.Equal(0, sut.GetModifier(Ability.Wisdom));

            //Passed Values
            Assert.Equal(18, sut.Scores[Ability.Strength]);
            Assert.Equal(4, sut.GetModifier(Ability.Strength));

            Assert.Equal(16, sut.Scores[Ability.Constitution]);
            Assert.Equal(3, sut.GetModifier(Ability.Constitution));

            Assert.Equal(12, sut.Scores[Ability.Charisma]);
            Assert.Equal(1, sut.GetModifier(Ability.Charisma));
        }

        [Fact]
        public void UpdateScore_WhenUpdatingScores_SucceedsCalculatingCorrectBonusesAndLeaveDefaultsUnchanged()
        {
            // Arrange
            var sut=new AbilityScores(abilityScores);

            // Act
            sut.UpdateScore(Ability.Strength, 20);
            sut.UpdateScore(Ability.Constitution, 18);
            sut.UpdateScore(Ability.Wisdom, 10);

            // Assert
            Assert.Equal(5, sut.GetModifier(Ability.Strength));
            Assert.Equal(4, sut.GetModifier(Ability.Constitution));
            Assert.Equal(0, sut.GetModifier(Ability.Wisdom));
        }

        [Fact]
        public void UpdateScore_WhenUpdatingScores_ShouldReplaceReadOnlyDictionaryReference()
        {
            // Arrange
            var sut = new AbilityScores(abilityScores);
            var initialScores = sut.Scores;
            
            const Ability abilityToUpdate = Ability.Dexterity;
            const int newScore = 14;

            // Act
            sut.UpdateScore(abilityToUpdate, newScore);
            var updatedScores = sut.Scores;

            // Assert
            Assert.NotSame(initialScores, updatedScores);
            Assert.Equal(12, initialScores[abilityToUpdate]);
            Assert.Equal(newScore, updatedScores[abilityToUpdate]);

        }
    }
}