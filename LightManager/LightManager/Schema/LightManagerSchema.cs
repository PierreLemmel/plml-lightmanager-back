using GraphQL.Types;
using System;
using System.Linq;

namespace LightManager.Api.Schema
{
    public class LightManagerSchema : GraphQL.Types.Schema
    {
        public LightManagerSchema(LightManagerQuery query, LightManagerMutation mutation)
        {
            Query = query;
            Mutation = mutation;
        }
    }
}