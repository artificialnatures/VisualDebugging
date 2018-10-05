namespace GeometryVisualizer
{
    public interface Scene
    {
        void SetBackgroundColor(float red, float green, float blue);

        void Pan(float deltaX, float deltaY);

        void Orbit(float deltaX, float deltaY);

        void Zoom(float delta);

        void AddMesh(VisualizerMesh mesh);
        
        void Reset();
    }
}