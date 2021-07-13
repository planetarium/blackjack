using System.Collections.Immutable;
using Libplanet;
using Libplanet.Assets;

namespace Stellarium.Models
{
    public sealed class Account
    {
        public Account(Address address)
        {
            Address = address;
        }

        public Address Address { get; }
    }
}
