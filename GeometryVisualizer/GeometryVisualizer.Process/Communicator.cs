namespace GeometryVisualizer.Process
{
    internal interface Communicator
    {
        void SendTransaction(Transaction transaction);
    }
}