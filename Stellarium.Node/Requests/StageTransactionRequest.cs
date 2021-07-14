namespace Stellarium.Node.Requests
{
    public class StageTransactionRequest
    {
        public byte[] Payload { get; set; }

        public StageTransactionRequest()
        {
            Payload = new byte[0];
        }
    }
}
