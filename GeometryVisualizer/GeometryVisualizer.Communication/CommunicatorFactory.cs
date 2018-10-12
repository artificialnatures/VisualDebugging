using System.Linq;
using System.Net.NetworkInformation;

namespace GeometryVisualizer.Communication
{
    public class CommunicatorFactory
    {
        public Communicator CreatePrimaryCommunicator(Serializer serializer)
        {
            return new PrimarySocketCommunicator(serializer, Constants.DefaultAddress, port);
        }
        
        public Communicator CreateSecondaryCommunicator(Serializer serializer)
        {
            return new SecondarySocketCommunicator(serializer, Constants.DefaultAddress, port);
        }

        private int FindOpenPort(int start)
        {
            var attemptsRemaining = 10;
            var openPort = start;
            var properties = IPGlobalProperties.GetIPGlobalProperties();
            var connections = properties.GetActiveTcpConnections();
            while (attemptsRemaining > 0)
            {
                var isOpen = connections.All(connection => connection.LocalEndPoint.Port != openPort);
                if (isOpen) return openPort;
                openPort++;
                attemptsRemaining--;
            }

            return start;
        }

        private int port = Constants.StartingPortNumber;
    }
}