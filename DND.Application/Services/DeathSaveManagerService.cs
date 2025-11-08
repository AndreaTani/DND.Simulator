using DND.Application.Contracts;

namespace DND.Application.Services
{
    public class DeathSaveManagerService : IDeathSaveManagerService
    {
        public Task InitializeDeathSavesAsync(Guid creatureId)
        {
            throw new NotImplementedException();
        }

        public Task RecordDeathSaveRollAsync(Guid creatureId, int rollValue)
        {
            throw new NotImplementedException();
        }
    }
}
