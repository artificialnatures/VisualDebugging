namespace GeometryVisualizer.Tests
{
    public class TestScene : Scene
    {
        public void SetBackgroundColor(float red, float green, float blue) { }
        
        public void Pan(float deltaX, float deltaY) { }

        public void Orbit(float deltaX, float deltaY) { }

        public void Zoom(float delta) { }

        public void AddMesh(VisualizerMesh mesh) { }

        public void Reset() { }
    }
}