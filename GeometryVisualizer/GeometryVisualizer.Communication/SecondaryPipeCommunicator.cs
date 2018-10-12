using System;
using System.IO;
using System.IO.Pipes;
using System.Threading.Tasks;

namespace GeometryVisualizer.Communication
{
    internal sealed class SecondaryPipeCommunicator : PipeCommunicator
    {
        public override void Connect()
        {
            if (pipe == null) return;
            Task.Run(() =>
            {
                ((NamedPipeClientStream) pipe).Connect();
                StartCommunicating();
            });
        }

        public override void Disconnect()
        {
            if (pipe == null) return;
            var secondary = (NamedPipeClientStream)pipe;
            if (secondary.IsConnected) secondary.Close();
            IsConnected = false;
        }
        
        public SecondaryPipeCommunicator(Serializer serializer) : base(serializer)
        {
            pipe = CreatePipe();
        }
        
        private PipeStream CreatePipe()
        {
            PipeStream pipeStream = null;
            try
            {
                pipeStream = new NamedPipeClientStream(".", pipeName, PipeDirection.InOut);
            }
            catch (IOException exception)
            {
                Console.WriteLine("Could not create secondary communicator: " + exception.Message);
            }

            return pipeStream;
        }
    }
}