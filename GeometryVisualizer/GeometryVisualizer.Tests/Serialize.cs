using GeometryVisualizer.Communication;
using Xunit;

namespace GeometryVisualizer.Tests
{
    public class Serialize
    {
        [Fact]
        public void CanSerializeAndDeserializeString()
        {
            var name = "Plane";
            var factory = new SerializerFactory();
            var serializer = factory.CreateSerializer();

            var stream = serializer.SerializeToStream(name);
            var deserializedName = serializer.Deserialize<string>(stream);
            
            Assert.Equal(name, deserializedName);
        }
        
        [Fact]
        public void CanSerializeAndDeserializeMesh()
        {
            var plane = TestGeometry.CreateUnitPlane("Plane");

            var factory = new SerializerFactory();
            var serializer = factory.CreateSerializer();
            
            var transaction = new Transaction(typeof(VisualizerMesh), serializer.SerializeToStream(plane));

            var stream = serializer.SerializeToStream(transaction);
            var deserializedTransaction = serializer.Deserialize<Transaction>(stream);
            
            Assert.Equal(typeof(VisualizerMesh).Name, transaction.PayloadType);
            
            var deserializedPlane = serializer.Deserialize<VisualizerMesh>(deserializedTransaction.Payload);
            
            Assert.Equal(plane.Label, deserializedPlane.Label);
        }
    }
}