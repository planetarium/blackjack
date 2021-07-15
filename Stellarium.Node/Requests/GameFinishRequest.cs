namespace Stellarium.Node.Requests
{
    public class GameFinishRequest
    {
        public GameFinishRequest(long stayed, string privateKey)
        {
            Stayed = stayed;
            PrivateKey = privateKey;
        }

        public long Stayed { set; get; }

        public string PrivateKey { set; get; }
    }
}
