using System;
using System.Collections.Generic;
using System.Linq;
using Bencodex.Types;

namespace Stellarium.Models.States
{
    public struct Position
    {
        public static readonly Position Origin = new Position(0, 0, 0);

        public Position(int x, int y, int z)
        {
            if (Math.Abs(x) > Galaxy.Size)
            {
                throw new ArgumentOutOfRangeException(nameof(x));
            }
            else if (Math.Abs(y) > Galaxy.Size)
            {
                throw new ArgumentOutOfRangeException(nameof(y));
            }
            else if (Math.Abs(z) > Galaxy.Size)
            {
                throw new ArgumentOutOfRangeException(nameof(z));
            }

            X = x;
            Y = y;
            Z = z;
        }

        public Position(Bencodex.Types.List serialized)
            : this((Integer)serialized[0], (Integer)serialized[1], (Integer)serialized[2])
        {
            if (serialized.Count > 3)
            {
                throw new ArgumentOutOfRangeException(nameof(serialized));
            }
        }

        public int Distance(Position other)
        {
            return Coordinates.Zip(
                other.Coordinates,
                (first, second) => Math.Abs(first - second)
            ).Max();
        }

        public IReadOnlyList<int> Coordinates => new int[] { X, Y, Z };

        public int X { get; }
        public int Y { get; }
        public int Z { get; }

        public Bencodex.Types.List Serialize() =>
            Bencodex.Types.List.Empty.Add(X).Add(Y).Add(Z);
    }
}
