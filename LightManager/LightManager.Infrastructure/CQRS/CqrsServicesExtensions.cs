using LightManager.Infrastructure.CQRS.Events;
using Microsoft.Extensions.DependencyInjection;


namespace LightManager.Infrastructure.CQRS
{
    public static class CqrsServicesExtensions
    {
        public static void AddCqrs(this IServiceCollection services)
        {
            services.AddSingleton<IEventStore, EventStore>();
        }
    }
}