using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Bencodex.Types;
using Libplanet;
using Libplanet.Action;

namespace Stellarium.Models.States
{
    public class Galaxy
    {
        // FIXME: Placeholder.
        public const uint Size = 100;

        private ImmutableDictionary<Position, Address> _tiles;

        public Galaxy(uint id, Address address)
        {
            Id = id;
            Address = address;
            _tiles = ImmutableDictionary<Position, Address>.Empty;
        }

        public uint Id { get; }

        public Address Address { get; }

        public void AddTile(Tile tile)
        {
            _tiles = _tiles.Add(tile.Position, tile.Address);
        }

        public IImmutableList<Tile> Tiles => GetTiles(address => null).Values.ToImmutableList();

        public IImmutableDictionary<Position, Tile> GetTiles(AccountStateGetter getter)
        {
            Dictionary<Position, Tile> result =
                new Dictionary<Position, Tile>();
            foreach ((Position position, Address address) in _tiles)
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