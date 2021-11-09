using LightManager.Infrastructure.CQRS.Commands;

namespace LightManager.Api.Controllers
{
    public record AddCommandRequest(CommandApiModel Command);
    public record AddCommandResponse(CommandResult Result);

    public record CommandApiModel(string CommandType, string CommandData);
}