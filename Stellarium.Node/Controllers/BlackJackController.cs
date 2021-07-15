using System;
using System.Net;
using Bencodex.Types;
using Libplanet;
using Libplanet.Crypto;
using Microsoft.AspNetCore.Mvc;
using Libplanet.Tx;
using Stellarium.Models.Actions;
using Stellarium.Models.States;
using Stellarium.Node.Services;
using Stellarium.Node.Requests;
using SAction = Libplanet.Action.PolymorphicAction<Stellarium.Models.Actions.BaseAction>;

namespace Stellarium.Node.Controllers
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

        [HttpPost]
        [Route("StageTx")]
        public Transaction<SAction> Post([FromBody] StageTransactionRequest request)
        {
            Transaction<SAction> tx = Transaction<SAction>.Deserialize(request.Payload);
            return tx;
        }

        [HttpPost]
        [Route("Start")]
        public Transaction<SAction> Post([FromBody]GameStartRequest request)
        {
            var privateKey = new PrivateKey(ByteUtil.ParseHex(request.PrivateKey));

            //TODO: build action
             var action = new ClickerAction(0);
             var tx = Transaction<SAction>.Create(
                 _swarmService.BlockChain.GetNextTxNonce(privateKey.ToAddress()),
                 privateKey,
                 _swarmService.BlockChain.Genesis.Hash,
                 new SAction[] {action},
                 null,
                 DateTimeOffset.UtcNow
             );
             _swarmService.BlockChain.StageTransaction(tx);
             return tx;
        }

        [HttpPost]
        [Route("Standby")]
        public Transaction<SAction> Post([FromBody]StandbyRequest request)
        {
            var privateKey = new PrivateKey(ByteUtil.ParseHex(request.PrivateKey));

            //TODO: build action
            var action = new ClickerAction(0);
            var tx = Transaction<SAction>.Create(
                _swarmService.BlockChain.GetNextTxNonce(privateKey.ToAddress()),
                privateKey,
                _swarmService.BlockChain.Genesis.Hash,
                new SAction[] {action},
                null,
                DateTimeOffset.UtcNow
            );
            _swarmService.BlockChain.StageTransaction(tx);
            return tx;
        }

        [HttpPost]
        [Route("Stay")]
        public Transaction<SAction> Post([FromBody]StayRequest request)
        {
            var privateKey = new PrivateKey(ByteUtil.ParseHex(request.PrivateKey));

            //TODO: build action
            var action = new ClickerAction(0);
            var tx = Transaction<SAction>.Create(
                _swarmService.BlockChain.GetNextTxNonce(privateKey.ToAddress()),
                privateKey,
                _swarmService.BlockChain.Genesis.Hash,
                new SAction[] {action},
                null,
                DateTimeOffset.UtcNow
            );
            _swarmService.BlockChain.StageTransaction(tx);
            return tx;
        }

        [HttpPost]
        [Route("Buy")]
        public Transaction<SAction> Post([FromBody]BuyRequest request)
        {
            var privateKey = new PrivateKey(ByteUtil.ParseHex(request.PrivateKey));

            //TODO: build action
            var action = new ClickerAction(0);
            var tx = Transaction<SAction>.Create(
                _swarmService.BlockChain.GetNextTxNonce(privateKey.ToAddress()),
                privateKey,
                _swarmService.BlockChain.Genesis.Hash,
                new SAction[] {action},
                null,
                DateTimeOffset.UtcNow
            );
            _swarmService.BlockChain.StageTransaction(tx);
            return tx;
        }

        [HttpGet]
        [Route("AccountState/{addressStr}")]
        public BlackJackAccountState Get(string addressStr)
        {
            var addr = new Address(ByteUtil.ParseHex(addressStr));
            return new BlackJackAccountState(
                addr,
                BlackJackAccountState.AccountStatus.StandBy,
                0,
                0
            );
        }
    }
}
