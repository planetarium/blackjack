using System;
using System.Runtime.Serialization;
using Libplanet;

namespace Blackjack.Models.Actions
{
    public class PermissionException : Exception
    {
        public Address Owner { get; private set; }
        public Address Claimer { get; private set; }

        public PermissionException(Address owner, Address claimer)
        {
            Owner = owner;
            Claimer = claimer;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(nameof(Owner), Owner);
            info.AddValue(nameof(Claimer), Claimer);
        }
    }
}
