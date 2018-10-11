using System.Collections.Generic;

namespace GeometryVisualizer.Tests
{
    public class TestScene : Scene
    {
        public IEnumerable<VisualizerNode> Nodes => nodes;
        
        public void SetBackgroundColor(float red, float green, float blue) { }
        
        public void Pan(float deltaX, float deltaY) { }

        public void Orbit(float deltaX, float deltaY) { }

        public void Zoom(float delta) { }

        public void AddMesh(VisualizerMesh mesh)
        {
            nodes.Add(mesh);
        }

        public void Reset() { }

        public TestScene()
        {
            nodes = new List<VisualizerNode>();
        }

        private List<VisualizerNode> nodes;
    }
}