using GraphQL;
using GraphQL.Types;
using GraphQL.NewtonsoftJson;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LightManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GraphQLController : Controller
    {
        private readonly IDocumentExecuter executer;
        private readonly IDocumentWriter writer;
        private readonly ISchema schema;

        public GraphQLController(
            IDocumentExecuter executer,
            IDocumentWriter writer,
            ISchema schema)
        {
            this.executer = executer;
            this.writer = writer;
            this.schema = schema;
        }

        [HttpPost("")]
        public async Task<ActionResult<object>> Get([FromBody] GraphQLRequest request)
        {
            ExecutionResult result = await executer.ExecuteAsync(new()
            {
                Schema = schema,
                Query = request.Query,
                OperationName = request.OperationName,
                Inputs = request.Variables?.ToInputs()
            });


            if (result.Errors?.Any() ?? false)
            {
                string errorMsg = result.Errors
                    .Select(err => err.Message)
                    .Join(Environment.NewLine);

                return BadRequest(errorMsg);
            }
            else
            {
                string json = await writer.WriteToStringAsync(result);
                
                return Ok(json);
            }
        }
    }
}