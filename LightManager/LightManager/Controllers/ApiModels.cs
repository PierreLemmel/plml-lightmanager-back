namespace LightManager.Api.Controllers
{
    public record AddCommandRequest(CommandApiModel Command);
    public record AddCommandResponse(CommandResultApiModel Result);

    public record CommandApiModel(string CommandType, string CommandData);
    public record CommandResultApiModel(bool Success, string? ErrorMessage);
}