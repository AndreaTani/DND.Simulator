namespace DND.Application.Contracts
{
    // Simple interface for logging service
    public interface ILoggingService
    {
        // Simple log method
        public Task LogMessageAsync(string message);
    }
}
