using System;
using System.Linq;
using Atomic.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionCommonExtensions
    {
        public static T GetSingletonInstanceOrNull<T>(this IServiceCollection services)
        {
            return (T)services
                .FirstOrDefault(d => d.ServiceType == typeof(T))
                ?.ImplementationInstance;
        }

        public static T GetSingletonInstance<T>(this IServiceCollection services)
        {
            var service = services.GetSingletonInstanceOrNull<T>();
            if (service == null)
            {
                throw new InvalidOperationException(
                    "Could not find singleton service: " + typeof(T).AssemblyQualifiedName);
            }

            return service;
        }

        public static IServiceCollection AddAtomicDependencyInjection(this IServiceCollection services)
        {
            return services.AddSingleton<ILazyServiceProvider, DefaultLazyServiceProvider>();
        }
    }
}