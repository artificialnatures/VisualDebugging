namespace GeometryVisualizer
{
    public interface Visualizer
    {
        void Send<T>(T data);

        void Receive();
    }
}