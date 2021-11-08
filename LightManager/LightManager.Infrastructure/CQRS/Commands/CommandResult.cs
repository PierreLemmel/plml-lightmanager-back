namespace LightManager.Infrastructure.CQRS.Commands
{
    public class CommandResult
    {
        public bool Success { get; }
        public string? ErrorMessage { get; }

        private CommandResult(bool success, string? errorMessage)
        {
            Success = success;
            ErrorMessage = errorMessage;
        }

        public static CommandResult Ok => new(true, null);
        public static CommandResult Error(string message) => new(false, message);
    }
}