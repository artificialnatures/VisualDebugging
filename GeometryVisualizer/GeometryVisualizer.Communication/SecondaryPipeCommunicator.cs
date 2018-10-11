using System.IO.Pipes;
using System.Threading.Tasks;

namespace GeometryVisualizer.Communication
{
    internal sealed class SecondaryPipeCommunicator : PipeCommunicator
    {
        public override void Connect()
        {
            Task.Run(() =>
            {
                ((NamedPipeClientStream) pipe).Connect();
                StartCommunicating();
            });
        }

        public override void Disconnect()
        {
            ((NamedPipeClientStream) pipe).Close();
        }
        
        public SecondaryPipeCommunicator(Serializer serializer) : base(serializer)
        {
            pipe = new NamedPipeClientStream(".", pipeName, PipeDirection.InOut);
        }
    }
}