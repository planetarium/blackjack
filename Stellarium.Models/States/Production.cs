using System.Numerics;
using System.Collections.Immutable;
using Libplanet.Assets;

namespace Stellarium.Models.States
{
    public sealed class Production
    {
        public Production(
            Currency resource,
            BigInteger productionAmount,
            BigInteger storageLimit)
        {
            Resource = resource;
            ProductionAmount = productionAmount;
            StorageLimit = storageLimit;
        }

        public Currency Resource { get; }
        public BigInteger ProductionAmount { get; }
        public BigInteger StorageLimit { get; }
    }
}
