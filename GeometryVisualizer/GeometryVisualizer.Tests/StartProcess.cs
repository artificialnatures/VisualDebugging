using GeometryVisualizer.Process;
using Xunit;

namespace GeometryVisualizer.Tests
{
    public class StartProcess
    {
        [Fact]
        public void CanStartVisualizerProcess()
        {
            var factory = new ProcessFactory();
            var visualizerProcess = factory.CreateVisualizerProcess(VisualizerType.Unity);
            visualizerProcess.Start();
            visualizerProcess.Stop();
        }

        [Fact]
        public void CanCommunicateWithProcess()
        {
            var factory = new ProcessFactory();
            var visualizerProcess = factory.CreateVisualizerProcess(VisualizerType.Unity);
            visualizerProcess.Start();
            //TODO: Create named pipe server and client...
            //TODO: Create transport structure for sending data...
            //TODO: Create a console app to test...
            visualizerProcess.Stop();
        }
    }
}