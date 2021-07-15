using System;
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
    public class ClickerTxController : ControllerBase
    {
        private readonly SwarmService<SAction> _swarmService;

        public ClickerTxController(SwarmService<SAction> swarmService)
        {
            _swarmService = swarmService;
        }

        [HttpPost]
        public Transaction<SAction> Post([FromBody]ClickerRequest request)
        {
            var action = new ClickerAction(request.Count);
            var privateKey = new PrivateKey(ByteUtil.ParseHex(request.PrivateKey));
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
        public ClickerState Get()
        {
            var state =
                _swarmService.StateStore.GetState(ClickerState.ClickerStateKey.ToHex().ToLowerInvariant(),
                    _swarmService.BlockChain.Tip.Hash);
            ClickerState clickerState;
            if (state is { } serialized)
            {
                clickerState = new ClickerState((Dictionary)serialized);
            }
            else
            {
                clickerState = new ClickerState();
            }

            return new ClickerState(clickerState.Count);
        }
    }
}
