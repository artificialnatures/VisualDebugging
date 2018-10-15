using System.Threading.Tasks;
using GeometryVisualizer.Communication;
using Xunit;

namespace GeometryVisualizer.Tests
{
    public class Communicate
    {
        [Fact]
        public void CanCommunicate()
        {
            var plane = TestGeometry.CreateUnitPlane("Plane");
            
            var serializerFactory = new SerializerFactory();
            var serializer = serializerFactory.CreateSerializer();
            
            var communicatorFactory = new CommunicatorFactory();

            var primaryCommunicator = communicatorFactory.CreatePrimaryCommunicator(serializer);
            primaryCommunicator.Connect();
            
            var secondaryCommunicator = communicatorFactory.CreateSecondaryCommunicator(serializer);
            secondaryCommunicator.Connect();

            Task.Delay(500).Wait();

            primaryCommunicator.Send(plane);

            Task.Delay(500).Wait();
            
            Assert.True(secondaryCommunicator.HasReceivedTransactions);

            var transaction = secondaryCommunicator.Receive();
            var deserializedPlane = serializer.Deserialize<VisualizerMesh>(transaction.Payload);
            
            Assert.Equal(plane.Label, deserializedPlane.Label);
            
            secondaryCommunicator.Disconnect();
            primaryCommunicator.Disconnect();
            
            Assert.False(primaryCommunicator.IsConnected);
            Assert.False(secondaryCommunicator.IsConnected);
        }

        [Fact]
        public void CanSendMultipleMessages()
        {
            var serializerFactory = new SerializerFactory();
            var serializer = serializerFactory.CreateSerializer();
            
            var communicatorFactory = new CommunicatorFactory();

            var primaryCommunicator = communicatorFactory.CreatePrimaryCommunicator(serializer);
            primaryCommunicator.Connect();
            
            var secondaryCommunicator = communicatorFactory.CreateSecondaryCommunicator(serializer);
            secondaryCommunicator.Connect();

            Task.Delay(500).Wait();
            
            for (var i = 0; i < 5; i++)
            {
                var geometryName = "Plane" + i;
                var plane = TestGeometry.CreateUnitPlane(geometryName);
                
                primaryCommunicator.Send(plane);
                Task.Delay(500).Wait();
                var transactionFromPrimary = secondaryCommunicator.Receive();
                var deserializedPlaneFromPrimary = serializer.Deserialize<VisualizerMesh>(transactionFromPrimary.Payload);
                
                secondaryCommunicator.Send(deserializedPlaneFromPrimary);
                Task.Delay(500).Wait();
                var transactionFromSecondary = primaryCommunicator.Receive();
                var deserializedPlaneFromSecondary = serializer.Deserialize<VisualizerMesh>(transactionFromSecondary.Payload);
                
                Assert.Equal(deserializedPlaneFromPrimary.Label, deserializedPlaneFromSecondary.Label);
            }
            
            secondaryCommunicator.Disconnect();
            primaryCommunicator.Disconnect();
        }

        [Fact]
        public void SecondaryWithoutPrimaryTimesOut()
        {
            var serializerFactory = new SerializerFactory();
            var serializer = serializerFactory.CreateSerializer();
            
            var communicatorFactory = new CommunicatorFactory();
            
            var secondaryCommunicator = communicatorFactory.CreateSecondaryCommunicator(serializer);
            secondaryCommunicator.Connect();

            Task.Delay(Constants.ConnectionTimeout + 1000).Wait();
            
            Assert.False(secondaryCommunicator.IsConnected);
            
            secondaryCommunicator.Disconnect();
        }

        [Fact]
        public void SecondaryTriesRepeatedlyToConnect()
        {
            var serializerFactory = new SerializerFactory();
            var serializer = serializerFactory.CreateSerializer();
            
            var communicatorFactory = new CommunicatorFactory();
            var primaryCommunicator = communicatorFactory.CreatePrimaryCommunicator(serializer);
            var secondaryCommunicator = communicatorFactory.CreateSecondaryCommunicator(serializer);
            secondaryCommunicator.Connect();

            Task.Delay(Constants.ConnectionTimeout * 3).Wait();
            Assert.False(secondaryCommunicator.IsConnected);
            
            primaryCommunicator.Connect();
            Task.Delay(Constants.ConnectionTimeout * 2).Wait();
            
            Assert.True(secondaryCommunicator.IsConnected);
            
            secondaryCommunicator.Disconnect();
            primaryCommunicator.Disconnect();
        }
    }
}