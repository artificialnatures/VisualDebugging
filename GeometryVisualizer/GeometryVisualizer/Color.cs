using System.Numerics;

namespace GeometryVisualizer
{
    internal class Color : VisualizerColor
    {
        public float Red => vector.X;
        
        public float Green => vector.Y;
        
        public float Blue => vector.Z;
        
        public float Alpha => vector.W;

        public Color(float red, float green, float blue, float alpha)
        {
            vector = new Vector4(red, green, blue, alpha);
        }

        private Vector4 vector;
    }
}