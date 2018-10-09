using System;
using System.Numerics;

namespace GeometryVisualizer
{
    [Serializable]
    internal class Vertex : VisualizerVertex
    {
        public float X => vector.X;

        public float Y => vector.Y;

        public float Z => vector.Z;

        public Vertex(float x, float y, float z)
        {
            vector = new Vector3(x, y, z);
        }
        
        private Vector3 vector;
    }
}