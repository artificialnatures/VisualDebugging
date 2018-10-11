using System.Linq;
using System.Threading.Tasks;
using GeometryVisualizer.Communication;
using Xunit;

namespace GeometryVisualizer.Tests
{
    public class CreateVisualizer
    {
        [Fact]
        public void CanCreateVisualizer()
        {
            var serializerFactory = new SerializerFactory();
            var serializer = serializerFactory.CreateSerializer();
            var communicatorFactory = new CommunicatorFactory();
            var communicator = communicatorFactory.CreatePrimaryCommunicator(serializer);
            var scene = new TestScene();
            var visualizerFactory = new VisualizerFactory();
            var visualizer = visualizerFactory.CreateVisualizer(scene, serializer, communicator);
            
            Assert.NotNull(visualizer);
        }
        
        [Fact]
        public void CanCommunicate()
        {
            var scene = new TestScene();
            var serializerFactory = new SerializerFactory();
            var serializer = serializerFactory.CreateSerializer();
            var communicatorFactory = new CommunicatorFactory();
            var primaryCommunicator = communicatorFactory.CreatePrimaryCommunicator(serializer);
            var secondaryCommunicator = communicatorFactory.CreateSecondaryCommunicator(serializer);
            var visualizerFactory = new VisualizerFactory();
            var primaryVisualizer = visualizerFactory.CreateVisualizer(scene, serializer, primaryCommunicator);
            secondaryCommunicator.Connect();

            var expectedLabel = "Plane";
            var plane = TestGeometry.CreateUnitPlane(expectedLabel);
            secondaryCommunicator.Send(plane);
            
            Task.Delay(500).Wait();
            
            primaryVisualizer.Receive();
            var nodes = scene.Nodes.ToArray();
            
            Assert.Equal(1, nodes.Length);
            Assert.Equal(expectedLabel, nodes[0].Label);
        }
    }
}
