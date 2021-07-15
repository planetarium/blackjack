using System;
using System.Runtime.Serialization;
using Libplanet;

namespace Blackjack.Models.Actions
{
    public class NotExistingException : Exception
    {
        public Address Address { get; private set; }

        public NotExistingException(Address address)
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
