namespace DND.Application.Contracts
{
    public interface IUIDispatcher
    {
        // Notifies the UI to refresh the creature
        // contained within the domainEvent
        public Task NotifyCharacterSheetRefreshAsync(Guid creatureId);
    }
}
