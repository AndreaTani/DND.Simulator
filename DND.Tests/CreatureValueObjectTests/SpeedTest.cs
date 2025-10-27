using DND.Domain.SharedKernel;

namespace DND.Tests.CreatureValueObjectTests
{
    public class SpeedTest
    {
        // Setup
        private static readonly int _walking = 25;
        private static readonly int _swimming = 10;
        private static readonly int _flying = 0;

        private static readonly int _defaultWalkingSpeed = 30;
        private static readonly int _defaultSwimmingSpeed = 0;
        private static readonly int _defaultFlyingSpeed = 0;

        [Fact]
        public void UpdateWalking_WhenPassedAPositiveValue_ShouldSucceed()
        {
            // Arrange
            var sut = new Speed(_walking, _swimming, _flying);
            int newWalking = 35;

            // Act
            bool result = sut.UpdateWalking(newWalking);
            int updatedWalking = sut.Walking;

            // Assert
            Assert.True(result);
            Assert.Equal(newWalking, updatedWalking);
        }

        [Fact]
        public void UpdateWalking_WhenPassedANegativeValue_ShouldReturnFalseAndNotUpdate()
        {
            // Arrange
            var sut = new Speed(_walking, _swimming, _flying);
            int originalWalking = sut.Walking;
            int newWalking = -5;

            // Act
            bool result = sut.UpdateWalking(newWalking);
            int updatedWalking = sut.Walking;

            // Assert
            Assert.False(result);
            Assert.Equal(originalWalking, updatedWalking);
            Assert.NotEqual(newWalking, updatedWalking);
        }

        [Fact]
        public void UpdateSwimming_WhenPassedAPositiveValue_ShouldSucceed()
        {
            // Arrange
            var sut = new Speed(_walking, _swimming, _flying);
            int newSwimming = 5;

            // Act
            bool result = sut.UpdateSwimming(newSwimming);
            int updatedSwimming = sut.Swimming;

            // Assert
            Assert.True(result);
            Assert.Equal(newSwimming, updatedSwimming);
        }

        [Fact]
        public void UpdateSwimming_WhenPassedANegativeValue_ShouldReturnFalseAndNotUpdate()
        {
            // Arrange
            var sut = new Speed(_walking, _swimming, _flying);
            var originalSwimming = sut.Swimming;
            int newSwimming = -5;

            // Act
            bool result = sut.UpdateSwimming(newSwimming);
            int updatedSwimming = sut.Swimming;

            // Assert
            Assert.False(result);
            Assert.Equal(originalSwimming, updatedSwimming);
            Assert.NotEqual(newSwimming, updatedSwimming);
        }

        [Fact]
        public void UpdateFlying_WhenPassedAPositiveValue_ShouldSucceed()
        {
            // Arrange
            var sut = new Speed(_walking, _swimming, _flying);
            int newFlying = 60;

            // Act
            bool result = sut.UpdateFlying(newFlying);
            int updatedFlying = sut.Flying;

            // Assert
            Assert.True(result);
            Assert.Equal(newFlying, updatedFlying);
        }

        [Fact]
        public void UpdateFlying_WhenPassedANegativeValue_ShouldReturnFlaseAndNotUpdate()
        {
            // Arrange
            var sut = new Speed(_walking, _swimming, _flying);
            int originalFlying = sut.Flying;
            int newFlying = -20;

            // Act
            bool result = sut.UpdateFlying(newFlying);
            int updatedFlying = sut.Flying;

            // Assert
            Assert.False(result);
            Assert.Equal(originalFlying, updatedFlying);
            Assert.NotEqual(newFlying, updatedFlying);
        }

        [Fact]
        public void Speed_WhenCreateWithNoParams_ShouldOnlyHaveWalkingSpeed()
        {
            // Arrange
            var sut = new Speed();

            // Act
            int walking = sut.Walking;
            int swimming = sut.Swimming;
            int flying = sut.Flying;

            // Assert
            Assert.Equal(_defaultWalkingSpeed, walking);
            Assert.Equal(_defaultSwimmingSpeed, swimming);
            Assert.Equal(_defaultFlyingSpeed, flying);
        }

        [Fact]
        public void Speed_WhenPassingNegativeValues_SholdThrow()
        {
            // Arrange
            int negaWalking = -1;
            int negaSwimming = -1;
            int negaFlying = -1;

            // Act
            void actWalkingSpeedNegative() => new Speed(negaWalking);
            void actSwimmingSpeedNegative() => new Speed(0, negaSwimming);
            void actFlyingSpeedNegative() => new Speed(0, 0, negaFlying);

            // Assert
            Assert.ThrowsAny<ArgumentException>(actWalkingSpeedNegative);
            Assert.ThrowsAny<ArgumentException>(actSwimmingSpeedNegative);
            Assert.ThrowsAny<ArgumentException>(actFlyingSpeedNegative);
        }

        [Fact]
        public void Speed_WhenCreatedWithAllParams_ShouldSetAllSpeedsCorrectly()
        {
            // Arrange
            int customWalk = 40;
            int customSwim = 20;
            int customFly = 80;

            // Act
            var sut = new Speed(customWalk, customSwim, customFly);

            // Assert
            Assert.Equal(customWalk, sut.Walking);
            Assert.Equal(customSwim, sut.Swimming);
            Assert.Equal(customFly, sut.Flying);
        }

    }
}
