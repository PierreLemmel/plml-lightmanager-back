using GraphQL.Types;
using LightManager.Infrastructure.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightManager.Api.Schema.Types
{
    public class UserType : ObjectGraphType<UserReadModel>
    {
        public UserType()
        {
            Name = "User";

            Field(u => u.Id, type: typeof(IdGraphType)).Description("User Id");
            Field(u => u.Name).Description("User name");
            Field(u => u.CreationTime).Description("Creation time");
            Field(u => u.ModificationTime, type: typeof(DateGraphType), nullable: true).Description("Modification time");
        }
    }
}
