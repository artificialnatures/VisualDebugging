using System.Collections.Concurrent;
using System.IO.Pipes;
using System.Threading.Tasks;

namespace GeometryVisualizer.Communication
{
    internal abstract class PipeCommunicator : Communicator
    {
        public bool IsConnected { get; protected set; }
        
        public bool HasReceivedTransactions => receiveQueue.Count > 0;

        public abstract void Connect();

        public abstract void Disconnect();

        public void Send<T>(T data)
        {
            sendQueue.Enqueue(new Transaction(typeof(T), serializer.SerializeToStream(data)));
        }

        public Transaction Receive()
        {
            if (receiveQueue.Count == 0) return null;
            receiveQueue.TryDequeue(out var transaction);
            return transaction;
        }

        protected PipeCommunicator(Serializer serializer)
        {
            this.serializer = serializer;
            sendQueue = new ConcurrentQueue<Transaction>();
            receiveQueue = new ConcurrentQueue<Transaction>();
        }

        protected void StartCommunicating()
        {
            SendContinuously();
            ReceiveContinuously();
            IsConnected = true;
        }

        private void ReceiveContinuously()
        {
            Task.Run(() =>
            {
                while (pipe.IsConnected)
                {
                    if (pipe.InBufferSize > 0)
                    {
                        receiveQueue.Enqueue(serializer.Deserialize<Transaction>(pipe));
                    }
                    
                    Task.Delay(100).Wait();
                }
            });
        }

        private void SendContinuously()
        {
            Task.Run(() =>
            {
                while (pipe.IsConnected)
                {
                    if (sendQueue.Count > 0)
                    {
                        sendQueue.TryDequeue(out var transaction);
                        var stream = serializer.SerializeToStream(transaction);
                        stream.CopyTo(pipe);
                        stream.Close();
                        pipe.Flush();
                    }

                    Task.Delay(100).Wait();
                }
            });
        }

        protected static readonly string pipeName = "GeometryVisualizer.Pipe";
        protected PipeStream pipe;
        
        private Serializer serializer;
        private ConcurrentQueue<Transaction> sendQueue;
        private ConcurrentQueue<Transaction> receiveQueue;
        private Task sendTask;
        private Task receiveTask;
    }
}