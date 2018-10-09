namespace GeometryVisualizer.Communication
{
    public class CommunicatorFactory
    {
        public Communicator CreateCommunicator()
        {
            var serializer = new BinarySerializer();
            return new PipeCommunicator(serializer);
        }
    }
}