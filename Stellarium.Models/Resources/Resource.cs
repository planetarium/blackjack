using Libplanet.Assets;

namespace Stellarium.Models.Resources
{
    public abstract class Resource
    {
        public static readonly Currency Gas =
            new Currency(ticker: "STG", decimalPlaces: 0, minter: null);

        public static readonly Currency Metal =
            new Currency(ticker: "STM", decimalPlaces: 0, minter: null);

        public Resource()
        {
        }
    }
}
