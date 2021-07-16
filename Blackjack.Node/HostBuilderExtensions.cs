using System.Linq;
using Blackjack.Node.Services;
using Libplanet.Action;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Blackjack.Node
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder UseSwarm<T>(this IHostBuilder hostBuilder)
            where T : IAction, new()
        {
            return hostBuilder.ConfigureServices((context, services) =>
            {
                IConfiguration configRoot = context.Configuration;
                services.Configure<SwarmServiceOptions>(configRoot.GetSection("SwarmService"));
                services.AddHostedService<SwarmService<T>>();
                services.AddSingleton(serviceProvider =>
                    serviceProvider.GetServices<IHostedService>().OfType<SwarmService<T>>().First());
            });
        }
    }
}
