using System;

namespace GeometryVisualizer
{
    [Serializable]
    internal class Vertex : VisualizerVertex
    {
        public float X { get; }

        public float Y { get; }

        public float Z { get; }

        public Vertex(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}