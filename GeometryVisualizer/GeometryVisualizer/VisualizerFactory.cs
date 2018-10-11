using GeometryVisualizer.Communication;

namespace GeometryVisualizer
{
    public class VisualizerFactory
    {
        public Visualizer CreateVisualizer(Scene scene, Serializer serializer, Communicator communicator)
        {
            return new BasicVisualizer(scene, serializer, communicator);
        }
    }
}