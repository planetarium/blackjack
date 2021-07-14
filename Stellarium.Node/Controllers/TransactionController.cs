using Microsoft.AspNetCore.Mvc;
using Libplanet.Tx;
using Stellarium.Node.Services;
using Stellarium.Node.Requests;
using SAction = Libplanet.Action.PolymorphicAction<Stellarium.Models.Actions.BaseAction>;

namespace Stellarium.Node.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly SwarmService<SAction> _swarmService;

        public TransactionController(SwarmService<SAction> swarmService)
        {
            _swarmService = swarmService;
        }

        [HttpPost]
        public void StageTransaction([FromBody]StageTransactionRequest request)
        {
            Transaction<SAction> transaction = Transaction<SAction>.Deserialize(request.Payload);
            _swarmService.BlockChain.StageTransaction(transaction);
        }
    }
}
