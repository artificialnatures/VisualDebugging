using System.Threading.Tasks;
using GeometryVisualizer.Process;
using Xunit;

namespace GeometryVisualizer.Tests
{
    public class StartProcess
    {
        [Fact]
        public void CanCommunicateWithProcess()
        {
            var factory = new ProcessFactory();
            var visualizerProcess = factory.CreateVisualizerProcess(VisualizerType.Unity);
            visualizerProcess.Start();
            var plane = TestGeometry.CreateUnitPlane("Plane");
            visualizerProcess.Communicator.Send(plane);
            Task.Delay(5000).Wait();
            visualizerProcess.Stop();
        }
    }
}