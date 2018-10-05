namespace GeometryVisualizer
{
    internal class BasicVisualizer : Visualizer
    {
        public BasicVisualizer(Scene externalScene)
        {
            scene = externalScene;
        }

        private Scene scene;
    }
}