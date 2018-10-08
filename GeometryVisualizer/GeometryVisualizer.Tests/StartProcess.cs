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
            //
            visualizerProcess.Stop();
        }
    }
}