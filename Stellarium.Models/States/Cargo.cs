using System;
using System.Numerics;

namespace Stellarium.Models.States
{
    public struct Cargo
    {
        public Cargo(BigInteger limit)
        {
            Limit = limit;
        }

        public BigInteger Limit { get; }
    }
}
