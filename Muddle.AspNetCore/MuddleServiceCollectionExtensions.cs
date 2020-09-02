using Microsoft.Extensions.DependencyInjection;

namespace Muddle.AspNetCore
{
    public static class MuddleServiceCollectionExtensions
    {
        public static void AddMuddle(this IServiceCollection services)
        {
            services.ConfigureOptions(typeof(MuddleConfigureOptions));
        }
    }
}
