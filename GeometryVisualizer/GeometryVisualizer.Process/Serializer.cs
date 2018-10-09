using System.IO;

namespace GeometryVisualizer.Process
{
    internal interface Serializer
    {
        Stream Serialize(object serializable);

        T Deserialize<T>(Stream stream);
    }
}