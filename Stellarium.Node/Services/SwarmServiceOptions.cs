using System;
using System.IO;
using Libplanet.Crypto;

namespace Stellarium.Node.Services
{
    public class SwarmServiceOptions
    {
        public bool Mine { get; set; }

        // In appsettings.json, the byte array should be base64-encoded.
        // See also: https://github.com/dotnet/runtime/pull/43150
        public byte[] PrivateKey { get; set; }

        public string StorePath { get; set; }

        public SwarmServiceOptions()
        {
            PrivateKey = new PrivateKey().ByteArray;
            StorePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "planetarium",
                "stellarium"
            );
        }
    }
}
