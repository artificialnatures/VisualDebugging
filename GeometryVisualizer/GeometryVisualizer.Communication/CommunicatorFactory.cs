namespace GeometryVisualizer.Communication
{
    public class CommunicatorFactory
    {
        public Communicator CreatePrimaryCommunicator(Serializer serializer)
        {
            return new PrimaryPipeCommunicator(serializer);
        }
        
        public Communicator CreateSecondaryCommunicator(Serializer serializer)
        {
            return new SecondaryPipeCommunicator(serializer);
        }
    }
}