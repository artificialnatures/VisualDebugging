using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace GeometryVisualizer.Communication
{
    internal sealed class SecondarySocketCommunicator : BaseCommunicator
    {
        public override void Connect()
        {
            Task.Run(() =>
            {
                client.Connect(address, port);
                OnConnected();
            });
        }

        public override void Disconnect()
        {
            IsConnected = false;
            client.Close();
        }
        
        public SecondarySocketCommunicator(Serializer serializer, string address, int port) : base(serializer)
        {
            client = new TcpClient();
            this.address = IPAddress.Parse(address);
            this.port = port;
        }

        private void OnConnected()
        {
            if (!client.Connected) return;
            IsConnected = true;
            clientStream = client.GetStream();
            Task.Run(() => SendAndReceive());
        }

        private void SendAndReceive()
        {
            while (IsConnected)
            {
                ReceiveFromSocket();
                Task.Delay(100).Wait();
                SendFromQueue();
                Task.Delay(100).Wait();
            }
        }

        private void ReceiveFromSocket()
        {
            if (!clientStream.DataAvailable) return;
            var bytes = serializer.GetBytes(clientStream);
            var transaction = serializer.Deserialize<Transaction>(bytes);
            receiveQueue.Enqueue(transaction);
        }

        private void SendFromQueue()
        {
            if (sendQueue.IsEmpty) return;
            sendQueue.TryDequeue(out var transaction);
            var bytes = serializer.SerializeToByteArray(transaction);
            clientStream.Write(bytes, 0, bytes.Length);
        }

        private TcpClient client;
        private NetworkStream clientStream;
        private IPAddress address;
        private int port;
    }
}