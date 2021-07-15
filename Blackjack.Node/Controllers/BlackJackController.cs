using System;
using System.Net;
using Bencodex.Types;
using Blackjack.Models.Actions;
using Blackjack.Models.States;
using Blackjack.Node.Services;
using Libplanet;
using Libplanet.Crypto;
using Microsoft.AspNetCore.Mvc;
using Libplanet.Tx;
using SAction = Libplanet.Action.PolymorphicAction<Blackjack.Models.Actions.BaseAction>;

namespace Blackjack.Node.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly SwarmService<SAction> _swarmService;

        public GameController(SwarmService<SAction> swarmService)
        {
            _swarmService = swarmService;
        }

        private Transaction<SAction> CreateTx(PrivateKey privateKey, SAction action)
            =>
                Transaction<SAction>.Create(
                    _swarmService.BlockChain.GetNextTxNonce(privateKey.ToAddress()),
                    privateKey,
                    _swarmService.BlockChain.Genesis.Hash,
                    new SAction[] {action},
                    null,
                    DateTimeOffset.UtcNow
                );

        [HttpPost]
        [Route("Standby")]
        public Transaction<SAction> Post([FromBody] StandbyRequest request)
        {
            var privateKey = new PrivateKey(ByteUtil.ParseHex(request.PrivateKey));
            var action = new StandbyAction(privateKey.ToAddress());
            var tx = CreateTx(privateKey, action);
            _swarmService.BlockChain.StageTransaction(tx);
            return tx;
        }

        [HttpPost]
        [Route("Start")]
        public Transaction<SAction> Post([FromBody] StartRequest request)
        {
            var privateKey = new PrivateKey(ByteUtil.ParseHex(request.PrivateKey));
            var action = new StartAction(privateKey.ToAddress());
            var tx = CreateTx(privateKey, action);
            _swarmService.BlockChain.StageTransaction(tx);
            return tx;
        }

        [HttpPost]
        [Route("Stay")]
        public Transaction<SAction> Post([FromBody] StayRequest request)
        {
            var privateKey = new PrivateKey(ByteUtil.ParseHex(request.PrivateKey));
            var action = new StayAction(privateKey.ToAddress(), request.Stayed);
            var tx = CreateTx(privateKey, action);
            _swarmService.BlockChain.StageTransaction(tx);
            return tx;
        }

        [HttpPost]
        [Route("Buy")]
        public Transaction<SAction> Post([FromBody] BuyRequest request)
        {
            var privateKey = new PrivateKey(ByteUtil.ParseHex(request.PrivateKey));

            //TODO: build action
            var action = new ClickerAction(0);
            var tx = CreateTx(privateKey, action);
            _swarmService.BlockChain.StageTransaction(tx);
            return tx;
        }

        [HttpGet]
        [Route("AccountState/{addressStr}")]
        public BlackJackAccountState Get(string addressStr)
        {
            var addr = new Address(ByteUtil.ParseHex(addressStr));
            var state = _swarmService.StateStore.GetState(addr.ToHex().ToLowerInvariant(),
                _swarmService.BlockChain.Tip.Hash);
            if (state is { })
            {
                return new BlackJackAccountState((Dictionary) state);
            }

            StatusCode((int) HttpStatusCode.NotFound);
            return new BlackJackAccountState(
                addr,
                BlackJackAccountState.AccountStatus.StandBy,
                0
            );
        }

        public class StartRequest
        {
            public StartRequest(string privateKey)
            {
                PrivateKey = privateKey;
            }

            public string PrivateKey { set; get; }
        }

        public class StandbyRequest
        {
            public StandbyRequest(string privateKey)
            {
                PrivateKey = privateKey;
            }

            public string PrivateKey { set; get; }
        }

        public class StayRequest
        {
            public StayRequest(string privateKey, int stayed)
            {
                PrivateKey = privateKey;
                Stayed = stayed;
            }

            public string PrivateKey { set; get; }

            public int Stayed { set; get; }
        }

        public class BuyRequest
        {
            public BuyRequest(string privateKey, int item)
            {
                PrivateKey = privateKey;
                Item = item;
            }

            public string PrivateKey { set; get; }

            public int Item { set; get; }
        }
    }
}
