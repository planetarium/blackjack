using System;
using System.Threading.Tasks;
using Libplanet.Blocks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Stellarium.Node.Services;
using SAction = Libplanet.Action.PolymorphicAction<Stellarium.Models.Actions.BaseAction>;

namespace Stellarium.Node
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
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
