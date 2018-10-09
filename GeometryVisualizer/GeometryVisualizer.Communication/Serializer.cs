using System.IO;

namespace GeometryVisualizer.Communication
{
    internal interface Serializer
    {
        Stream Serialize(object serializable);

        T Deserialize<T>(Stream stream);
    }
}