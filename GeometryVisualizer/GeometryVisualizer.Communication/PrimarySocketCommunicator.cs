using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace GeometryVisualizer.Communication
{
    internal sealed class PrimarySocketCommunicator : BaseCommunicator
    {
        public override void Connect()
        {
            listener.Start();
            IsConnected = true;
            Listen();
            Task.Run(() => SendAndReceive());
        }

        public override void Disconnect()
        {
            IsConnected = false;
            CloseAllConnections();
        }
        
        public PrimarySocketCommunicator(Serializer serializer, string address, int port) : base(serializer)
        {
            connections = new List<TcpClient>();
            listener = new TcpListener(IPAddress.Parse(address), port);
        }

        private void Listen()
        {
            listener.BeginAcceptSocket(AddConnection, listener);
        }

        private void AddConnection(IAsyncResult result)
        {
            if (!IsConnected) return;
            var connection = listener.EndAcceptTcpClient(result);
            connections.Add(connection);
        }

        private void SendAndReceive()
        {
            while (IsConnected)
            {
                connections.ForEach(ReceiveFromConnection);
                Task.Delay(100).Wait();
                SendToAll();
                Task.Delay(100).Wait();
            }
        }

        private void SendToAll()
        {
            if (sendQueue.IsEmpty) return;
            sendQueue.TryDequeue(out var transaction);
            var bytes = serializer.SerializeToByteArray(transaction);
            connections.ForEach(connection => SendToConnection(connection, bytes));
        }

        private void SendToConnection(TcpClient connection, byte[] bytes)
        {
            if (!connection.Connected) return;
            connection.GetStream().Write(bytes, 0, bytes.Length);
        }

        private void ReceiveFromConnection(TcpClient connection)
        {
            if (!connection.Connected || connection.Available == 0) return;
            var bytes = serializer.GetBytes(connection.GetStream());
            var transaction = serializer.Deserialize<Transaction>(bytes);
            receiveQueue.Enqueue(transaction);
        }

        private void CloseAllConnections()
        {
            connections.ForEach(connection => connection.Close());
            listener.Stop();
        }

        private TcpListener listener;
        private List<TcpClient> connections;
    }
}