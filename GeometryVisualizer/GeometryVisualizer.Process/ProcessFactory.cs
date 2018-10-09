using GeometryVisualizer.Communication;

namespace GeometryVisualizer.Process
{
    public class ProcessFactory
    {
        public VisualizerProcess CreateVisualizerProcess(VisualizerType visualizerType)
        {
            var factory = new CommunicatorFactory();
            var communicator = factory.CreateCommunicator();
            if (visualizerType == VisualizerType.Unity) return new UnityVisualizerProcess(communicator);
            return null;
        }
    }
}