using System.IO.Pipes;

namespace GeometryVisualizer.Communication
{
    internal class PipeCommunicator : Communicator
    {
        public void SendTransaction(Transaction transaction)
        {
            throw new System.NotImplementedException();
        }

        public PipeCommunicator(Serializer serializer)
        {
            this.serializer = serializer;
        }

        private Serializer serializer;
        private NamedPipeServerStream pipe;
    }
}