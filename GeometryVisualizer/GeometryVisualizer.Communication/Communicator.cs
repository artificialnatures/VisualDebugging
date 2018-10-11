using System;

namespace GeometryVisualizer.Communication
{
    public interface Communicator
    {
        void Connect();

        void Disconnect();

        void Send<T>(T data);

        bool HasReceivedTransactions { get; }

        Transaction Receive();
    }
}