using System.IO.Pipes;
using System.Threading.Tasks;

namespace GeometryVisualizer.Communication
{
    internal sealed class PrimaryPipeCommunicator : PipeCommunicator
    {
        public override void Connect()
        {
            Task.Run(() =>
            {
                ((NamedPipeServerStream) pipe).WaitForConnection();
                StartCommunicating();
            });
        }

        public override void Disconnect()
        {
            ((NamedPipeServerStream) pipe).Disconnect();
        }
        
        public PrimaryPipeCommunicator(Serializer serializer) : base(serializer)
        {
            pipe = new NamedPipeServerStream(pipeName, PipeDirection.InOut);
        }
    }
}