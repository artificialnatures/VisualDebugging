namespace GeometryVisualizer
{
    public static class VisualizerFactory
    {
        public static Visualizer CreateVisualizer(Scene scene)
        {
            return new BasicVisualizer(scene);
        }
    }
}