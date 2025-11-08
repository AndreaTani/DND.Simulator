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
        public static TheoryData<int, int, int, List<Condition>> HPStatusChange
        {
            get
            {
                var data = new TheoryData<int, int, int, List<Condition>>
                {
                    { 10, -15, 100, [Condition.Unconscious, Condition.Dying] },
                    { 10, -62, 50, [Condition.Dead] },
                    { 30, -20, 50, [Condition.None] }
                };
                return data;
            }
        }

        [Theory]
        [MemberData(nameof(HPStatusChange))]
        public async Task Handle_CreatureHPChangedEvent_ShouldUpdateStatusBasedOnHP(int previusHp, int amount, int maxHp, List<Condition> conditions)
        {
            // Arrange
            var creatureServiceMock = new Mock<ICreatureService>();
            var loggingServiceMock = new Mock<ILoggingService>();
            var handler = new CreatureHPDrivenStatusHandler(creatureServiceMock.Object, loggingServiceMock.Object);

            var domainEvent = new CreatureHPChangedEvent(
                CreatureId: Guid.NewGuid(),
                CreatureName: "Test Creature",
                MaxHp: maxHp,
                PreviousHp: previusHp,
                Amount: amount,
                CurrentHp: previusHp + amount,
                Type: DamageType.Fire
            );

            var expectedMessage = $"Creature {domainEvent.CreatureId} HP changed from {domainEvent.PreviousHp} to {domainEvent.CurrentHp} (Change: {domainEvent.Amount}, Type: {domainEvent.Type})";

            // Act
            await handler.Handle(domainEvent);

            // Assert
            loggingServiceMock.Verify(m => m.Log(
                It.Is<string>(s =>
                s.Contains(domainEvent.CreatureId.ToString()) &&
                s.Contains(domainEvent.CreatureName) &&
                s.Contains(domainEvent.PreviousHp.ToString()) &&
                s.Contains(domainEvent.CurrentHp.ToString()) &&
                s.Contains(domainEvent.Amount.ToString()) &&
                s.Contains(domainEvent.Type.ToString())
            )), Times.Once);

            creatureServiceMock.Verify(m => m.HandleCreatureHpStatusAsync(
                domainEvent.CreatureId,
                domainEvent.MaxHp,
                domainEvent.CurrentHp
            ), Times.Once);

            if (!conditions.Contains(Condition.None))
            {
                creatureServiceMock.Verify(m => m.ApplyConditionsAsync(
                domainEvent.CreatureId,
                conditions
                ), Times.Once);
            }
        }
    }
}