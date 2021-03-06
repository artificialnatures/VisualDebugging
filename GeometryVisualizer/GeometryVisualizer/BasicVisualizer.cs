using GeometryVisualizer.Communication;

namespace GeometryVisualizer
{
    internal class BasicVisualizer : Visualizer
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

        public BasicVisualizer(Scene scene, Serializer serializer, Communicator communicator)
        {
            this.scene = scene;
            this.serializer = serializer;
            this.communicator = communicator;
            this.communicator?.Connect();
        }

        private void RouteTransaction(Transaction transaction)
        {
            if (transaction.PayloadType == typeof(VisualizerMesh).Name)
            {
                scene.AddMesh(serializer.Deserialize<Mesh>(transaction.Payload));
            }

            if (transaction.PayloadType == typeof(string).Name)
            {
                var message = serializer.Deserialize<string>(transaction.Payload);
                switch (message)
                {
                    case "quit":
                        communicator.Disconnect();
                        break;
                }
            }
        }

        private Scene scene;
        private Serializer serializer;
        private Communicator communicator;
    }
}