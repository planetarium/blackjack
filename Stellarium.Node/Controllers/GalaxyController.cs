using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stellarium.Models.States;
using Libplanet;
using Libplanet.Assets;

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
            Cost cost = new Cost(rng.Next(), ImmutableDictionary<Currency, BigInteger>.Empty);
            Durability durability = new Durability(rng.Next(), rng.Next());
            Energy energy = new Energy(rng.Next(), rng.Next());
            Building building = new Building(galaxyId, new Address(), new Address(), Position.Origin, cost, durability, energy);
            galaxy.AddTile(building);
            return galaxy;
        }
    }
}
