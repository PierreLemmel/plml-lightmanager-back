﻿using GraphQL;
using GraphQL.NewtonsoftJson;
using GraphQL.Types;
using LightManager.Api.Schema;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace LightManager.Api
{
    public static class Bootstraper
    {
        public static void InitializeServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LightManager", Version = "v1" });
            });

            services.AddGraphQLSchema();
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