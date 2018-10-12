using System.Collections.Concurrent;

namespace GeometryVisualizer.Communication
{
    internal abstract class BaseCommunicator : Communicator
    {
        public bool IsConnected { get; protected set; }
        
        public bool HasReceivedTransactions => !receiveQueue.IsEmpty;

        public abstract void Connect();

        public abstract void Disconnect();

        public void Send<T>(T data)
        {
            sendQueue.Enqueue(new Transaction(typeof(T), serializer.SerializeToStream(data)));
        }

        public Transaction Receive()
        {
            if (receiveQueue.IsEmpty) return null;
            receiveQueue.TryDequeue(out var transaction);
            return transaction;
        }

        protected BaseCommunicator(Serializer serializer)
        {
            this.serializer = serializer;
            sendQueue = new ConcurrentQueue<Transaction>();
            receiveQueue = new ConcurrentQueue<Transaction>();
        }
        
        protected Serializer serializer;
        protected ConcurrentQueue<Transaction> sendQueue;
        protected ConcurrentQueue<Transaction> receiveQueue;
    }
}