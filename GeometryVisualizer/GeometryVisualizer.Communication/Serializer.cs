using System;
using System.IO;

namespace GeometryVisualizer.Communication
{
    public interface Serializer
    {
        Stream SerializeToStream(object serializable);

        byte[] SerializeToByteArray(object serializable);

        T Deserialize<T>(Stream stream);

        T Deserialize<T>(Byte[] serialized);

        byte[] GetBytes(Stream stream);
    }
}