using UnityEngine;
using GeometryVisualizer;
using GeometryVisualizer.Communication;
using GeometryVisualizer.Unity;

namespace GeometryVisualizer.Unity
{
	internal class Bootstrapper : MonoBehaviour 
	{
		void Start ()
		{
			var sceneFactory = new UnitySceneFactory();
			scene = sceneFactory.CreateScene();
			var visualizerFactory = new VisualizerFactory();
			visualizer = visualizerFactory.CreateVisualizer(scene);
		}

		void Update()
		{
			visualizer.Receive();
		}

		private Scene scene;
		private Visualizer visualizer;
		private Communicator communicator;
	}
}