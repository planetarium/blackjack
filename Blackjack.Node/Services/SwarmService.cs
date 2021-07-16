using System;
using System.Threading;
using System.Threading.Tasks;
using Libplanet.Action;
using Libplanet.Net;
using Libplanet.Blockchain;
using Libplanet.Blockchain.Policies;
using Libplanet.Blocks;
using Microsoft.Extensions.Hosting;
using Libplanet.RocksDBStore;
using System.IO;
using Libplanet.Store;
using Libplanet;
using Libplanet.Crypto;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using Libplanet.Blockchain.Renderers;
using System.Collections.Generic;

namespace Blackjack.Node.Services
{
    public class SwarmService<T> : BackgroundService
        where T : IAction, new()
    {
        public IStore Store { get; }

        public IStateStore StateStore { get; }

        public BlockChain<T> BlockChain { get; }

        public Swarm<T> Swarm { get; }

        public bool Mine { get; }

        private PrivateKey _minerPrivateKey;

        private CancellationTokenSource? _miningCancellationTokenSource;

        public SwarmService(IOptions<SwarmServiceOptions> options)
        {
            SwarmServiceOptions opts = options.Value;
            Mine = opts.Mine;
            _minerPrivateKey = new PrivateKey(opts.PrivateKey);
            var blockPolicy = new BlockPolicy<T>();
            var stagePolicy = new VolatileStagePolicy<T>();
            string storePath = opts.StorePath;
            Store = new RocksDBStore(storePath);
            var stateKeyValueStore = new RocksDBKeyValueStore(Path.Combine(storePath, "states"));
            var stateHashKeyValueStore = new RocksDBKeyValueStore(Path.Combine(storePath, "state_hashes"));
            StateStore = new TrieStateStore(stateKeyValueStore, stateHashKeyValueStore);
            BlockChain = new BlockChain<T>(
                policy: blockPolicy,
                stagePolicy: stagePolicy,
                store: Store,
                stateStore: StateStore,
                genesisBlock: LoadGenesisBlock(),
                renderers: new IRenderer<T>[] {
                    new AnonymousRenderer<T>()
                    {
                        BlockRenderer = (oldTip, newTip) =>
                        {
                        }
                    }
                }
            );
            var apv = new AppProtocolVersion();
            Swarm = new Swarm<T>(
                BlockChain,
                _minerPrivateKey,
                apv,
                trustedAppProtocolVersionSigners: null,
                iceServers: new[] { ParseIceServer("turn://0ed3e48007413e7c2e638f13ddd75ad272c6c507e081bd76a75e4b7adc86c9af:0apejou+ycZFfwtREeXFKdfLj2gCclKzz5ZJ49Cmy6I=@9c-turn.planetarium.dev:3478") },
                differentAppProtocolVersionEncountered: null,
                options: new SwarmOptions
                {
                    MaxTimeout = TimeSpan.FromSeconds(10),
                    BlockHashRecvTimeout = TimeSpan.FromSeconds(10),
                    BlockRecvTimeout = TimeSpan.FromSeconds(1),
                    BranchpointThreshold = 50,
                    StaticPeers = null,
                }
            );
        }

        public static Block<T> MineGenesisBlock()
        {
            return BlockChain<T>.MakeGenesisBlock(hashAlgorithm: HashAlgorithmType.Of<SHA256>());
        }

        public static Block<T> LoadGenesisBlock()
        {
            const string encoded = "ZDE6SGQxOgBpMWUxOlRpMGUxOmMzMjrR" +
                "MfkDUkces3Lq8299Cx6yWxtricACpy7j5n4dyhGweDE6ZGkwZTE6aDMyOhYoH64SibGCJVVFwnsDzX/" +
                "RTpgzA57QNrd2OSo8OdFpMTppaTBlMTptMjA6+jkDL4uRZD+g8VPw1jq1uDjoAm4xOm4xMDo4cMVycM" +
                "2wFsWAMTpzMzI6Gxax31OLoS3D+X7buFyqcFDUbBSBNCkP66gPgjbIPbkxOnR1Mjc6MjAyMS0wNy0xM" +
                "1QwNToyNToyMi43MDYyNjRaMTp4MzI6/HvQbN2mbZ9tUenseJ48XL3JkqVnPqE66XVmj0Y+bttlMTpU" +
                "bDIyNjpkMTpTNzE6MEUCIQD/SItxaG9FaeVkm3N8yoVM1cjskD9zA8LKNzDKPVSGswIgC1tEBNoYSI5" +
                "Ggym0tFg7+R+657pVuagLrNxz0dWA6bcxOmFsZTE6bmkwZTE6cDY1OgSZfGIJeGC+d1BorgkThZlNyK" +
                "f8pVmzj680k/9ee3MXp4jq/9p/VAqRqWC4skLMxJ6M/9x2tDXEDUEMkdIOcFqzMTpzMjA6+jkDL4uRZ" +
                "D+g8VPw1jq1uDjoAm4xOnR1Mjc6MjAyMS0wNy0xM1QwNToyNToyMi42NTI5MzZaMTp1bGVlZWU=";

            var serialized = Convert.FromBase64String(encoded);
            return Block<T>.Deserialize(serialized);
        }

        public static IceServer ParseIceServer(string iceServerInfo)
        {
            // FIXME: This should be done by Libplanet...
            // See also: https://github.com/planetarium/libplanet/issues/1381
            var uri = new Uri(iceServerInfo);
            string[] userInfo = uri.UserInfo.Split(':');

            return new IceServer(new[] {uri}, userInfo[0], userInfo[1]);
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await base.StartAsync(cancellationToken);
            await Swarm.PreloadAsync(cancellationToken: cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var tasks = new List<Task>();
            tasks.Add(Swarm.StartAsync(cancellationToken: stoppingToken));

            if (Mine)
            {
                _miningCancellationTokenSource =
                    CancellationTokenSource.CreateLinkedTokenSource(stoppingToken);
                var mineTask = Task.Run(async () =>
                {
                    try
                    {
                        CancellationToken cancellationToken = _miningCancellationTokenSource.Token;
                        Address minerAddress = _minerPrivateKey.ToAddress();
                        while (!stoppingToken.IsCancellationRequested)
                        {
                            await BlockChain.MineBlock(
                                minerAddress,
                                cancellationToken: cancellationToken
                            );
                            Thread.Sleep(5000);
                        }
                    }
                    finally
                    {
                        _miningCancellationTokenSource.Dispose();
                        _miningCancellationTokenSource = null;
                    }
                },
                _miningCancellationTokenSource.Token);
                tasks.Add(mineTask);
            }

            await Task.WhenAll(tasks);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await Swarm.StopAsync(cancellationToken);
            await base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            Swarm.Dispose();
            if (StateStore is IDisposable disposableStateStore)
            {
                disposableStateStore.Dispose();
            }
            if (Store is IDisposable disposableStore)
            {
                disposableStore.Dispose();
            }

            base.Dispose();
        }
    }
}
