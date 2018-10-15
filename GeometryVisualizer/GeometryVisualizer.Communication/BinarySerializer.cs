using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GeometryVisualizer.Communication
{
    internal class BinarySerializer : Serializer
    {
        public Stream SerializeToStream(object serializable)
        {
            return GetStream(serializable);
        }

        public byte[] SerializeToByteArray(object serializble)
        {
            using (var stream = GetStream(serializble))
            {
                Rewind(stream);
                var bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                return bytes;
            }
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

        public byte[] GetBytes(Stream stream)
        {
            var buffer = new byte[Constants.BufferSize];
            var dataLength = stream.Read(buffer, 0, buffer.Length);
            var bytes = new byte[dataLength];
            Array.Copy(buffer, bytes, bytes.Length);
            return bytes;
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