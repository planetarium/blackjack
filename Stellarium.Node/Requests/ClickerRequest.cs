namespace Stellarium.Node.Requests
{
    public class ClickerRequest
    {
        public ClickerRequest(long count, string privateKey)
        {
            Count = count;
            PrivateKey = privateKey;
        }

        public long Count { set; get; }

        public string PrivateKey { set; get; }
    }
}
