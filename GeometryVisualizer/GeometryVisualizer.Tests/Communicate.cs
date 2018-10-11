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

            secondaryCommunicator.Send(plane);

            Task.Delay(500).Wait();
            
            Assert.True(primaryCommunicator.HasReceivedTransactions);

            var transaction = primaryCommunicator.Receive();
            var deserializedPlane = serializer.Deserialize<VisualizerMesh>(transaction.Payload);
            
            Assert.Equal(plane.Label, deserializedPlane.Label);
        }
    }
}