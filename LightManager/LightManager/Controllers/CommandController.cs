using LightManager.Infrastructure.CQRS.Commands;
using LightManager.Infrastructure.CQRS.Events;
using Microsoft.AspNetCore.Mvc;

namespace LightManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommandController : ControllerBase
{
    private readonly ICommandDispatcher commandDispatcher;
    private readonly ICommandDataMapping commandDataMapping;
    private readonly ICommandStore commandStore;
    private readonly IEventDispatcher eventDispatcher;
    private readonly IEventStore eventStore;

    public CommandController(
        ICommandDispatcher commandDispatcher,
        ICommandDataMapping commandDataMapping,
        ICommandStore commandStore,
        IEventDispatcher eventDispatcher,
        IEventStore eventStore)
    {
        this.commandDispatcher = commandDispatcher;
        this.commandDataMapping = commandDataMapping;
        this.commandStore = commandStore;
        this.eventDispatcher = eventDispatcher;
        this.eventStore = eventStore;
    }

    [HttpPost("")]
    public async Task<ActionResult<AddCommandResponse>> Add([FromBody] AddCommandRequest request)
    {
        (string commandType, string commandData) = request.Command;
        DateTime commandTime = DateTime.Now;

        var command = commandDataMapping.MapCommand(commandTime, commandType, commandData);
        CommandResult commandResult = await commandDispatcher.Send(command);

        CommandResultApiModel resultModel = new(commandResult.Success, commandResult.ErrorMessage);
        AddCommandResponse response = new(resultModel);

        await commandStore.Add(command, commandResult);

        if (commandResult.Success)
        {
            foreach (Event @event in commandResult.Events)
            {
                await eventDispatcher.Send(@event);
                await eventStore.Add(@event);
            }

            return Ok(response);
        }
        else
        {
            return StatusCode(500, response);
        }
        
    }
}