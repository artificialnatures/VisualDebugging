using System;
using System.IO;
using System.IO.Pipes;
using System.Threading.Tasks;

namespace GeometryVisualizer.Communication
{
    internal sealed class PrimaryPipeCommunicator : PipeCommunicator
    {
        public override void Connect()
        {
            if (pipe == null) return;
            Task.Run(() =>
            {
                ((NamedPipeServerStream) pipe).WaitForConnection();
                StartCommunicating();
            });
        }

        public override void Disconnect()
        {
            if (pipe == null) return;
            var primary = (NamedPipeServerStream)pipe;
            if (primary.IsConnected) primary.Disconnect();
            IsConnected = false;
        }
        
        public PrimaryPipeCommunicator(Serializer serializer) : base(serializer)
        {
            pipe = CreatePipe();
        }

        private PipeStream CreatePipe()
        {
            PipeStream pipeStream = null;
            try
            {
                pipeStream = new NamedPipeServerStream(pipeName, PipeDirection.InOut);
            }
            catch (IOException exception)
            {
                Console.WriteLine("Could not create primary communicator: " + exception.Message);
            }

            return pipeStream;
        }
    }
}