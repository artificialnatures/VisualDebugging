namespace GeometryVisualizer.Communication
{
    public class SerializerFactory
    {
        public Serializer CreateSerializer()
        {
            return new BinarySerializer();
        }
    }
}