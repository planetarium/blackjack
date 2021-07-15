namespace Stellarium.Node.Requests
{
    public class GameStartRequest
    {
        public GameStartRequest(string privateKey)
        {
            PrivateKey = privateKey;
        }

        public string PrivateKey { set; get; }
    }
}
