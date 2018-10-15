using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace GeometryVisualizer.Communication
{
    internal sealed class SecondarySocketCommunicator : BaseCommunicator
    {
        public override void Connect()
        {
            Task.Run(() => StartConnecting());
        }

        public override void Disconnect()
        {
            shouldAttemptToConnect = false;
            IsConnected = false;
            client.Close();
        }
        
        public SecondarySocketCommunicator(Serializer serializer, string address, int port) : base(serializer)
        {
            client = new TcpClient();
            this.address = IPAddress.Parse(address);
            this.port = port;
            shouldAttemptToConnect = true;
        }

        private async void StartConnecting()
        {
            while (shouldAttemptToConnect)
            {
                await TryConnecting();
                if (IsConnected) break;
            }
        }

        private async Task<bool> TryConnecting()
        {
            var tokenSource = new CancellationTokenSource();
            tokenSource.CancelAfter(Constants.ConnectionTimeout);
            return await Task.Run(() =>
            {
                try
                {
                    client.Connect(address, port);
                    return OnConnected();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    return false;
                }
            }, tokenSource.Token);
        }

        private bool OnConnected()
        {
            if (!client.Connected) return false;
            IsConnected = true;
            clientStream = client.GetStream();
            Task.Run(() => SendAndReceive());
            Console.WriteLine("Secondary communicator connected to primary.");
            return true;
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
        private bool shouldAttemptToConnect;
    }
}