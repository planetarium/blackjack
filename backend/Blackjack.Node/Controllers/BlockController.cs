using Blackjack.Node.Services;
using Microsoft.AspNetCore.Mvc;
using Libplanet.Tx;
using SAction = Libplanet.Action.PolymorphicAction<Blackjack.Models.Actions.BaseAction>;

namespace Blackjack.Node.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlockController : ControllerBase
    {
        private readonly SwarmService<SAction> _swarmService;

        public BlockController(SwarmService<SAction> swarmService)
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

        [HttpGet]
        [Route("Height")]
        public long Get()
        {
            return _swarmService.BlockChain.Tip.Index;
        }

        public class StageTransactionRequest
        {
            public byte[] Payload { get; set; }

            public StageTransactionRequest()
            {
                Payload = new byte[0];
            }
        }
    }
}
