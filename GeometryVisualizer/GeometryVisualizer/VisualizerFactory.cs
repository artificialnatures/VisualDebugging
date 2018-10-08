namespace GeometryVisualizer
{
    public class VisualizerFactory
    {
        public Visualizer CreateVisualizer(Scene scene)
        {
            return new BasicVisualizer(scene);
        }
    }
}