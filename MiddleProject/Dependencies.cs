using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace MiddleProject
{
    public static class Dependencies
    {
        public static IServiceCollection RegisterRequestHandlers(
            this IServiceCollection services)
        {
            return services.AddMediatR(typeof(Dependencies).Assembly);
        }
    }
}
