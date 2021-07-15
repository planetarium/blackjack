namespace Stellarium.Node.Requests
{
    public class StandbyRequest
    {
        public StandbyRequest(string privateKey)
        {
            PrivateKey = privateKey;
        }

        public string PrivateKey { set; get; }
    }
}
