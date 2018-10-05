using System.Collections.Generic;

namespace GeometryVisualizer
{
    public interface VisualizerMesh : VisualizerNode
    {
        VisualizerColor Color { get; }
        
        IEnumerable<VisualizerVertex> Vertices { get; }
        
        IEnumerable<IEnumerable<int>> TriangleIndices { get; }
    }
}