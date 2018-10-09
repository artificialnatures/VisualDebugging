using System.Collections.Generic;

namespace GeometryVisualizer
{
    public class GeometryFactory
    {
        public void SetColor(float red, float green, float blue, float alpha)
        {
            color = new Color(red, green, blue, alpha);
        }

        public void AddVertex(float x, float y, float z)
        {
            vertices.Add(new Vertex(x, y, z));
        }

        public void AddTriangle(int index0, int index1, int index2)
        {
            triangleIndices.Add(new List<int>{ index0, index1, index2 });
        }
        
        public void BeginMesh(string label)
        {
            geometryType = GeometryType.Mesh;
            Reset(label);
        }

        public VisualizerMesh CompleteMesh()
        {
            return new Mesh(geometryLabel, color, vertices, triangleIndices);
        }
        
        public GeometryFactory() : this("Geometry") { }

        public GeometryFactory(string label)
        {
            Reset(label);
        }

        private void Reset(string label)
        {
            geometryLabel = label;
            color = new Color(0f, 0f, 0f, 1f);
            vertices = new List<VisualizerVertex>();
            triangleIndices = new List<List<int>>();
        }

        private GeometryType geometryType = GeometryType.Mesh;
        private string geometryLabel;
        private VisualizerColor color;
        private List<VisualizerVertex> vertices;
        private List<List<int>> triangleIndices;
    }
}