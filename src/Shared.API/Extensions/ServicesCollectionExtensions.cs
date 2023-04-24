using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.API.Extensions
{
    public static class ServicesCollectionExtensions
    {
        public static void AddUserInfo(this IServiceCollection services)
        {
            services.AddScoped(serviceProvider =>
            {
                var httpContext = serviceProvider.GetService<IHttpContextAccessor>().HttpContext;
                return httpContext?.CurrentUser();
            });
        }
    }
}
