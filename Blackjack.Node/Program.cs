using System;
using System.Threading.Tasks;
using Blackjack.Models.Actions;
using Blackjack.Node.Services;
using Jint;
using Libplanet.Blocks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using SAction = Libplanet.Action.PolymorphicAction<Blackjack.Models.Actions.BaseAction>;

namespace Blackjack.Node
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json")
                .Build();
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            if (Environment.GetEnvironmentVariable("STELLARIUM_MINE_GENESIS") == "1")
            {
                // For development purposes
                MineGenesisBlock(args);
            }
            else
            {
                await RunHost(args);
            }
        }

        public static void MineGenesisBlock(string[] args)
        {
            Block<SAction> genesis = SwarmService<SAction>.MineGenesisBlock();
            Console.WriteLine(Convert.ToBase64String(genesis.Serialize()));
        }

        public static async Task RunHost(string[] args)
        {
            try
            {
                await CreateHostBuilder(args).Build().RunAsync();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseSwarm<SAction>()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
