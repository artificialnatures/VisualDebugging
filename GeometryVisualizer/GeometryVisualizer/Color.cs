using System;

namespace GeometryVisualizer
{
    [Serializable]
    internal class Color : VisualizerColor
    {
        public float Red { get; }
        
        public float Green { get; }
        
        public float Blue { get; }
        
        public float Alpha { get; }

        public Color(float red, float green, float blue, float alpha)
        {
            Red = red;
            Green = green;
            Blue = blue;
            Alpha = alpha;
        }
    }
}