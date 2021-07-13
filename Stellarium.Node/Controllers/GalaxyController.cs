using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Libplanet;
using Libplanet.Assets;
using Stellarium.Models.States;
using Stellarium.Models.States.Ledgers;
using Stellarium.Models.States.Units;

namespace Stellarium.Node.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GalaxyController : ControllerBase
    {
        private readonly ILogger<GalaxyController> _logger;

        public GalaxyController(ILogger<GalaxyController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Galaxy Get()
        {
            var rng = new Random();
            uint galaxyId = (uint)rng.Next();
            Galaxy galaxy = new Galaxy(galaxyId, new Address());
            TotalCost totalCost = new TotalCost(new Cost(Resource.Metal, 100), new Cost(Resource.Gas, 50));
            Consumed consumed = new Consumed(new Address(), new Address(), totalCost);
            Requirement requirement = new Requirement(1);
            PhysicalStat physicalStat = new PhysicalStat(100, 50, 10);
            Energy energy = new Energy(50, 25);
            Ship ship = new Ship(galaxyId, new Address(), new Address(), Position.Origin, consumed, requirement, physicalStat, energy);
            galaxy.AddTile(ship);
            return galaxy;
        }
    }
}
