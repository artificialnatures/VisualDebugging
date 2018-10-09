using GeometryVisualizer.Communication;

namespace GeometryVisualizer.Process
{
    public interface VisualizerProcess
    {
        Communicator Communicator { get; }
        
        void Start();
        
        void Stop();
    }
}