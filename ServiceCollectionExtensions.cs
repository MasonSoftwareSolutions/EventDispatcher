using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MasonSoftwareSolutions.EventDispatcher
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventDispatcher(this IServiceCollection services, params Assembly[] assemblies)
        {
            return services
                .AddScoped<IEventDispatcher, EventDispatcher>()
                .Scan(s => s.FromAssemblies(assemblies)
                    .AddClasses(c => c
                        .AssignableTo(typeof(IEventHandler<>)))
                        .AsImplementedInterfaces()
                    .AddClasses(c => c
                        .AssignableTo(typeof(IDeferredEventHandler<>)))
                        .AsImplementedInterfaces());
        }
    }
}
