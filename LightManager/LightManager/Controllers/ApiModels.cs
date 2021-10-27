using Newtonsoft.Json.Linq;

namespace LightManager.Api.Controllers
{
    public record GraphQLRequest(string? OperationName, string Query, JObject? Variables);
}