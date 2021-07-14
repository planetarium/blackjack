using Bencodex.Types;
using Microsoft.AspNetCore.Mvc;
using Stellarium.Models.States;
using Stellarium.Node.Services;
using SAction = Libplanet.Action.PolymorphicAction<Stellarium.Models.Actions.BaseAction>;

namespace Stellarium.Node.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClickController : ControllerBase
    {
        private readonly SwarmService<SAction> _swarmService;
        public ClickController(SwarmService<SAction> swarmService)
        {
            _swarmService = swarmService;
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
