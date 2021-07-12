using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Bencodex.Types;
using Libplanet;
using Libplanet.Action;

namespace Stellarium.Models
{
    public class Galaxy
    {
        // FIXME: Placeholder.
        public const uint Size = 100;

        private ImmutableDictionary<(uint Left, uint Top), Address> _tiles;

        public Galaxy(uint id)
        {
            Id = id;
            _tiles = ImmutableDictionary<(uint Left, uint Top), Address>.Empty;
        }

        public uint Id { get; }

        public IImmutableDictionary<(uint Left, uint Top), Tile> GetTiles(AccountStateGetter getter)
        {
            Dictionary<(uint Left, uint Top), Tile> result =
                new Dictionary<(uint Left, uint Top), Tile>();
            foreach (((uint left, uint top), Address address) in _tiles)
            {
                IValue? serialized = getter(address);
                if (serialized is { } s)
                {
                    // Tile t = TileSerializer.Deserialize(serialized);
                    // result.Add((left, top), t);
                }
            }
            return result.ToImmutableDictionary();
        }
    }
}
