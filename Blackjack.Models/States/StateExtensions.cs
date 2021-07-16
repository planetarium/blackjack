using System;
using System.Collections.Generic;
using System.Linq;
using Bencodex.Types;
using Libplanet;

namespace Blackjack.Models.States
{
    public static class StateExtensions
    {
        public static IValue Serialize<T>(Func<T, IValue> serializer, T? value)
            where T : struct
        {
            return value is T v ? serializer(v) : default(Null);
        }

        public static T? Deserialize<T>(Func<IValue, T> deserializer, IValue serialized)
            where T : struct
        {
            return serialized is Null ? (T?)null : deserializer(serialized);
        }

        public static IValue Serialize<T>(this IEnumerable<T> values)
            where T : IValue
        {
            return new List(values.Cast<IValue>());
        }

        public static IEnumerable<T> ToEnumerable<T>(this IValue serialized, Func<IValue, T> deserializer)
        {
            return ((List)serialized).Select(deserializer);
        }

        public static T[] ToArray<T>(this IValue serialized, Func<IValue, T> deserializer)
        {
            return serialized.ToEnumerable(deserializer).ToArray();
        }

        public static List<T> ToList<T>(this IValue serialized, Func<IValue, T> deserializer)
        {
            return serialized.ToEnumerable(deserializer).ToList();
        }

        public static HashSet<T> ToHashSet<T>(this IValue serialized, Func<IValue, T> deserializer)
        {
            return new HashSet<T>(serialized.ToEnumerable(deserializer));
        }
        public static IValue Serialize(this Address address) =>
            new Binary(address.ToByteArray());

        public static IValue Serialize(this Address? address) =>
            Serialize(Serialize, address);

        public static Address ToAddress(this IValue serialized) =>
            new Address(((Binary)serialized).ToByteArray());

        public static Address? ToNullableAddress(this IValue serialized) =>
            Deserialize(ToAddress, serialized);

    }
}
