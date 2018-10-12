using GeometryVisualizer.Communication;

namespace GeometryVisualizer.Process
{
    public class ProcessFactory
    {
        public VisualizerProcess CreateVisualizerProcess(VisualizerType visualizerType)
        {
            var serializerFactory = new SerializerFactory();
            var serializer = serializerFactory.CreateSerializer();
            var communicatorFactory = new CommunicatorFactory();
            var communicator = communicatorFactory.CreatePrimaryCommunicator(serializer);
            if (visualizerType == VisualizerType.Unity) return new UnityVisualizerProcess(communicator);
            if (visualizerType == VisualizerType.UnityEditor) return new UnityEditorVisualizerProcess(communicator);
            return null;
        }
    }
}