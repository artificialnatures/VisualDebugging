using System;

namespace GeometryVisualizer.Communication
{
    public interface Communicator
    {
        bool IsConnected { get; }
        
        void Connect();

        void Disconnect();

        void Send<T>(T data);

        bool HasReceivedTransactions { get; }

        Transaction Receive();
    }
}