using GraphQL;
using GraphQL.NewtonsoftJson;
using GraphQL.Types;
using LightManager.Api.Schema;
using LightManager.Infrastructure.CQRS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace LightManager.Api
{
    public static class Bootstraper
    {
        public static void InitializeServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LightManager", Version = "v1" });
            });

            IReadOnlyCollection<Type> eventTypes = new List<Type>() { };
            IReadOnlyCollection<Type> commandTypes = new List<Type>() { };

            services.AddCqrs(eventTypes, commandTypes);
            services.AddGraphQLSchema();

            services.AddSingleton(configuration);
        }

        private static void AddGraphQLSchema(this IServiceCollection services)
        {
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();

            services.AddSingleton<ISchema>(sp => sp.GetRequiredService<LightManagerSchema>());
            services.AddSingleton<LightManagerSchema>();


            services.AddSingleton<LightManagerQuery>();
            services.AddSingleton<LightManagerMutation>();
        }
    }
}
