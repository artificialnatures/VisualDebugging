using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GeometryVisualizer.Process
{
    internal class BinarySerializer : Serializer
    {
        public Stream Serialize(object serializable)
        {
            var stream = Stream.Null;
            binaryFormatter.Serialize(stream, serializable);
            return stream;
        }

        public T Deserialize<T>(Stream stream)
        {
            var deserialized = binaryFormatter.Deserialize(stream);
            return (T)deserialized;
        }

        public BinarySerializer()
        {
            binaryFormatter = new BinaryFormatter();
        }

        private BinaryFormatter binaryFormatter;
    }
}