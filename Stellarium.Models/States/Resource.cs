using System.Numerics;
using Libplanet.Assets;

namespace Stellarium.Models.States
{
    public static class Resource
    {
        // FIXME: Placeholder.
        public const long MetalWeight = 1;

        public const long GasWeight = 1;

        public static readonly Currency Gas =
            new Currency(ticker: "STG", decimalPlaces: 0, minter: null);

        public static readonly Currency Metal =
            new Currency(ticker: "STM", decimalPlaces: 0, minter: null);
    }
}
