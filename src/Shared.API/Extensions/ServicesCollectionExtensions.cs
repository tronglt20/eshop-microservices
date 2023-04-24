using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Shared.Domain.Interfaces;
using Shared.Infrastructure;
using System.Reflection;

namespace Shared.API.Extensions
{
    public static class ServicesCollectionExtensions
    {
        public static void AddUserInfo(this IServiceCollection services)
        {
            services.AddScoped(serviceProvider =>
            {
                var httpContext = serviceProvider.GetService<IHttpContextAccessor>()?.HttpContext;
                return httpContext?.CurrentUser();
            });
        }

        public static IServiceCollection AddImplementationInterfaces(this IServiceCollection services
           , Type interfaceType
           , Type implementAssemblyType)
        {
            var implementTypes = Assembly.GetAssembly(implementAssemblyType).GetTypes().Where(_ =>
                        _.IsClass
                        && !_.IsAbstract
                        && !_.IsInterface
                        && !_.IsGenericType
                        && _.GetInterface(interfaceType.Name) != null);

            foreach (var implementType in implementTypes)
            {
                var mainInterfaces = implementType
                    .GetInterfaces()
                    .Where(_ => _.GenericTypeArguments.Count() == 0);

                foreach (var mainInterface in mainInterfaces)
                {
                    services.AddScoped(mainInterface, implementType);
                }
            }

            return services;
        }
    }
}
