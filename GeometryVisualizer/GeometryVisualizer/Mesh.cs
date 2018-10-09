using System;
using System.Collections.Generic;

namespace GeometryVisualizer
{
    [Serializable]
    internal class Mesh : VisualizerMesh
    {
        public string Label { get; }
        
        public VisualizerColor Color { get; }
        
        public IEnumerable<VisualizerVertex> Vertices { get; }
        
        public IEnumerable<IEnumerable<int>> TriangleIndices { get; }

        public Mesh(string label, VisualizerColor color, IEnumerable<VisualizerVertex> vertices, IEnumerable<IEnumerable<int>> triangleIndices)
        {
            Label = label;
            Color = color;
            Vertices = vertices;
            TriangleIndices = triangleIndices;
        }
    }
}