namespace DND.Application.Contracts
{
    // Simple interface for logging service
    public interface ILoggingService
    {
        // Simple log method
        public Task Log(string message);
    }
}
