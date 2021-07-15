using System;
using System.Collections.Immutable;
using System.Security.Cryptography;
using Libplanet;

namespace Blackjack.Models
{
    public static class AddressExtensions
    {
        /// <summary>
        /// Derives a new <see cref="Address"/> (in deterministic way) from the given
        /// <paramref name="address"/> and the input <paramref name="bytes"/>.
        /// </summary>
        public static Address Derive(this in Address address, in ReadOnlySpan<byte> bytes)
        {
            SHA1 hasher = SHA1.Create();
            byte[] input = new byte[Address.Size + bytes.Length];
            address.ByteArray.CopyTo(input, 0);
            bytes.CopyTo(input.AsSpan(Address.Size));
            byte[] derived = hasher.ComputeHash(input);
            return new Address(derived);
        }

        /// <summary>
        /// Derives a new <see cref="Address"/> (in deterministic way) from the given
        /// <paramref name="address"/> and the input <paramref name="bytes"/>.
        /// </summary>
        public static Address Derive(this in Address address, ImmutableArray<byte> bytes) =>
            Derive(address, bytes.AsSpan());

        /// <summary>
        /// Derives a new <see cref="Address"/> (in deterministic way) from the given
        /// <paramref name="address"/> and the input <paramref name="bytes"/>.
        /// </summary>
        public static Address Derive(this in Address address, byte[] bytes) =>
            Derive(address, bytes.AsSpan());

        /// <summary>
        /// Derives a new <see cref="Address"/> (in deterministic way) from the given
        /// <paramref name="address"/> and the <paramref name="input"/> unsigned 32-bit integer.
        /// </summary>
        public static Address Derive(this in Address address, uint input) =>
            Derive(address, new byte[] { Convert.ToByte(input) });
    }
}
