using Microsoft.Extensions.DependencyInjection;
using Muddle.Domain.Models;

namespace Muddle.AspNetCore
{
    public static class MuddleServiceCollectionExtensions
    {
        public static void AddMuddle(this IServiceCollection services)
        {
            services.ConfigureOptions(typeof(MuddleConfigureOptions));
            services.AddSingleton<IGamesMaster, GamesMaster>();
        }
    }
}
