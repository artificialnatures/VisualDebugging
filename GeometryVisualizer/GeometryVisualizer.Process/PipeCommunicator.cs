using System.IO.Pipes;

namespace GeometryVisualizer.Process
{
    internal class PipeCommunicator : Communicator
    {
        public void SendTransaction(Transaction transaction)
        {
            throw new System.NotImplementedException();
        }

        public PipeCommunicator()
        {
            serializer = new BinarySerializer();
        }

        private Serializer serializer;
        private NamedPipeServerStream pipe;
    }
}