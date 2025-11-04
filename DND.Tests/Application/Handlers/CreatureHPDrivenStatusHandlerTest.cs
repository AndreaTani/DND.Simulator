using DND.Application.Contracts;
using DND.Application.Handlers;
using DND.Domain.SharedKernel;
using Moq;

namespace DND.Tests.Application.Handlers
{
    /// <summary>
    /// When the HP of a creature changes, this handler should check if the new
    /// HP value is lesser than or euqal to zero, if so check if it is  lesser
    /// than or equal to negative max HP, in the former case apply 'Unconscious'
    /// status, in the latter case apply 'Dead' status.
    /// </summary>
    public class CreatureHPDrivenStatusHandlerTest
    {
        [Theory]
        [InlineData(10, -15, 100, Condition.Unconscious)]
        [InlineData(10, -62, 50, Condition.Dead)]
        [InlineData(30, -20, 50, Condition.None)]
        public async Task Handle_CreatureHPChangedEvent_ShouldUpdateStatusBasedOnHP(int previusHp, int amount, int maxHp, Condition condition)
        {
            // Arrange
            var sut = new Mock<ICreatureService>();
            var handler = new CreatureHPDrivenStatusHandler(sut.Object);

            var domainEvent = new CreatureHPChangedEvent(
                CreatureId: Guid.NewGuid(),
                MaxHp: maxHp,
                PreviousHp: previusHp,
                Amount: amount,
                CurrentHp: previusHp + amount,
                Type: DamageType.Fire
            );

            // Act
            await handler.Handle(domainEvent);

            // Assert
            sut.Verify(m => m.HandleCreatureHpStatusAsync(
                domainEvent.CreatureId,
                domainEvent.MaxHp,
                domainEvent.CurrentHp
            ), Times.Once);

            if (condition != Condition.None)
            {
                sut.Verify(m => m.ApplyConditionsAsync(
                domainEvent.CreatureId,
                condition
                ), Times.Once);
            }
        }
    }
}