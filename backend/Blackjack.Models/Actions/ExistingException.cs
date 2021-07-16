using System;
using System.Runtime.Serialization;
using Libplanet;

namespace Blackjack.Models.Actions
{
    public class ExistingException : Exception
    {
        public Address Address { get; private set; }

        public ExistingException(Address address)
        {
            Address = address;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(nameof(Address), Address);
 }
    }
}
