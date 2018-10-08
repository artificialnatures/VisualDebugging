using UnityEngine;
using GeometryVisualizer;
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

		private Scene scene;
		private Visualizer visualizer;
	}
}