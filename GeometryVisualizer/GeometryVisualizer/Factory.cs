namespace GeometryVisualizer
{
    public static class Factory
    {
        public static Visualizer CreateVisualizer()
        {
            return new UnityVisualizer();
        }
    }
}