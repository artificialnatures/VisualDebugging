using GeometryVisualizer.Communication;
using UnityEngine;

namespace GeometryVisualizer.Unity
{
    internal class UnityVisualizer : UnityNode, Visualizer
    {
        public void Send<T>(T data)
        {
            communicator?.Send(data);
        }

        public void Receive()
        {
            if (communicator == null || !communicator.HasReceivedTransactions) return;
            RouteTransaction(communicator.Receive());
        }
        
        public UnityVisualizer(Scene scene, Serializer serializer, Communicator communicator) : base("Visualizer")
        {
            this.scene = scene;
            this.serializer = serializer;
            this.communicator = communicator;
            this.communicator.Connect();
        }
        
        private void RouteTransaction(Transaction transaction)
        {
            if (transaction.PayloadType == typeof(VisualizerMesh).Name)
            {
                scene.AddMesh(serializer.Deserialize<VisualizerMesh>(transaction.Payload));
            }

            if (transaction.PayloadType == typeof(string).Name)
            {
                var message = serializer.Deserialize<string>(transaction.Payload);
                if (message == "quit")
                {
                    communicator.Disconnect();
                    Application.Quit();
                }
            }
        }

        private Scene scene;
        private Serializer serializer;
        private Communicator communicator;
    }
}