using System;
using System.IO;

namespace GeometryVisualizer.Communication
{
    public interface Serializer
    {
        Stream Serialize(object serializable);

        T Deserialize<T>(Stream stream);

        T Deserialize<T>(Byte[] serialized);
    }
}