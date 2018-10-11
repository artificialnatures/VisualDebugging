namespace GeometryVisualizer.Tests
{
    public static class TestGeometry
    {
        public static VisualizerMesh CreateUnitPlane(string label)
        {
            var factory = CreateGeometryFactory();
            factory.BeginMesh(label);
            factory.AddVertex(-0.5f, -0.5f, 0.0f);
            factory.AddVertex(0.5f, -0.5f, 0.0f);
            factory.AddVertex(0.5f, 0.5f, 0.0f);
            factory.AddVertex(-0.5f, 0.5f, 0.0f);
            factory.AddTriangle(0, 1, 2);
            factory.AddTriangle(0, 2, 3);
            return factory.CompleteMesh();
        }
        
        private static GeometryFactory CreateGeometryFactory()
        {
            return new GeometryFactory();
        }
    }
}