using System.Threading.Tasks;
using UnityEngine;
using GeometryVisualizer.Communication;

namespace GeometryVisualizer.Unity
{
	internal class Bootstrapper : MonoBehaviour 
	{
		void Start ()
		{
			var factory = new UnityFactory();
			scene = factory.CreateScene();
			var serializerFactory = new SerializerFactory();
			var serializer = serializerFactory.CreateSerializer();
			var communicatorFactory = new CommunicatorFactory();
			communicator = communicatorFactory.CreateSecondaryCommunicator(serializer);
			visualizer = factory.CreateVisualizer(scene, serializer, communicator);
		}

		void Update()
		{
			visualizer?.Receive();
		}

		void OnApplicationQuit()
		{
			communicator.Disconnect();
		}

		private Scene scene;
		private Visualizer visualizer;
		private Communicator communicator;
	}
}