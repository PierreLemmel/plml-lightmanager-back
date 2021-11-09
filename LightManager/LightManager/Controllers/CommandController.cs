using LightManager.Infrastructure.CQRS.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommandController : ControllerBase
    {
        private readonly ICommandDispatcher commandDispatcher;
        private readonly ICommandDataMapping commandDataMapping;

        public CommandController(
            ICommandDispatcher commandDispatcher,
            ICommandDataMapping commandDataMapping)
        {
            this.commandDispatcher = commandDispatcher;
            this.commandDataMapping = commandDataMapping;
        }

        [HttpPost("")]
        public async Task<ActionResult<AddCommandResponse>> Add([FromBody] AddCommandRequest request)
        {
            (string commandType, string commandData) = request.Command;
            DateTime commandTime = DateTime.Now;

            var command = commandDataMapping.MapCommand(commandTime, commandType, commandData);
            CommandResult commandResult = await commandDispatcher.Send(command);

            AddCommandResponse response = new(commandResult);
            if (commandResult.Success)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(500, response);
            }
            
        }
    }
}
