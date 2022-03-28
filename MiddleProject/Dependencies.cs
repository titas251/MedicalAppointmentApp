using Microsoft.Extensions.DependencyInjection;
using MediatR;
using MiddleProject.Models.MapperProfiles;

namespace MiddleProject
{
    public static class Dependencies
    {
        public static IServiceCollection RegisterRequestHandlers(
            this IServiceCollection services)
        {
            return services.AddMediatR(typeof(Dependencies).Assembly);
        }

        public static IServiceCollection RegisterMapper(
            this IServiceCollection services)
        {
            services.AddAutoMapper(c =>
            {
                c.AddProfile<WebMappingProfile>();
            }, typeof(Dependencies));

            return services;
        }
    }
}
