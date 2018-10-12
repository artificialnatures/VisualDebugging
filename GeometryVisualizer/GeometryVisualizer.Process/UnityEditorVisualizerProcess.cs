using System;
using GeometryVisualizer.Communication;

namespace GeometryVisualizer.Process
{
    public class UnityEditorVisualizerProcess : VisualizerProcess
    {
        public Communicator Communicator { get; }

        public void Start()
        {
            Communicator.Connect();
            Console.WriteLine("Communicator started. Press play in Unity Editor.");
        }

        public void Stop()
        {
            Communicator.Disconnect();
        }

        public UnityEditorVisualizerProcess(Communicator communicator)
        {
            Communicator = communicator;
        }
    }
}