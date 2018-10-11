using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GeometryVisualizer.Communication
{
    internal class BinarySerializer : Serializer
    {
        public Stream Serialize(object serializable)
        {
            return GetStream(serializable);
        }

        public T Deserialize<T>(Stream stream)
        {
            Rewind(stream);
            var deserialized = binaryFormatter.Deserialize(stream);
            stream.Close();
            return (T)deserialized;
        }

        public T Deserialize<T>(Byte[] serialized)
        {
            var stream = new MemoryStream(serialized);
            Rewind(stream);
            var deserialized = Deserialize<T>(stream);
            stream.Close();
            return deserialized;
        }

        public BinarySerializer()
        {
            binaryFormatter = new BinaryFormatter();
        }

        private Stream GetStream(object serializable)
        {
            var stream = new MemoryStream();
            binaryFormatter.Serialize(stream, serializable);
            Rewind(stream);
            return stream;
        }

        private void Rewind(Stream stream)
        {
            if (stream.CanSeek) stream.Seek(0, SeekOrigin.Begin);
        }

        private BinaryFormatter binaryFormatter;
    }
}