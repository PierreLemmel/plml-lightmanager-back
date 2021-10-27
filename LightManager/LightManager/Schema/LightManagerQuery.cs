using GraphQL.Types;
using LightManager.Api.Schema.Types;
using LightManager.Infrastructure.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightManager.Api.Schema
{
    public class LightManagerQuery : ObjectGraphType
    {
        public LightManagerQuery()
        {
            Field<UserType>(
                "User",
                description: "Current application user",
                arguments: new(),
                resolve: ctx =>
                {
                    UserReadModel result = new UserReadModel("foooo", "Pierre Lemmel", DateTime.Now, null);
                    return result;
                });
        }
    }
}