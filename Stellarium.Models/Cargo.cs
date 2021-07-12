using System;

namespace Stellarium.Models
{
    public struct Cargo
    {
        public Cargo(ulong limit, ulong metal, ulong gas)
        {
            if (metal + gas > limit)
            {
                throw new ArgumentOutOfRangeException();
            }

            Limit = limit;
            Metal = metal;
            Gas = gas;
        }

        public Cargo Load(ulong metal, ulong gas)
            => new Cargo(Limit, Metal + metal, Gas + gas);

        public ulong Limit { get; }
        public ulong Metal { get; }
        public ulong Gas { get; }
        public ulong Weight => Metal + Gas;
    }
}
