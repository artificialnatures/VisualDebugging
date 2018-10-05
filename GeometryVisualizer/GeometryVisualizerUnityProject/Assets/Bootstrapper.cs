using UnityEngine;
using GeometryVisualizer;
using GeometryVisualizer.Unity;

namespace GeometryVisualizer.Unity
{
	public class Bootstrapper : MonoBehaviour 
	{
		public void ReportMouseMovement(Vector3 movement)
		{
			
		}
		
		void Start ()
		{
			scene = UnitySceneFactory.CreateScene();
			var plane = CreatePlane();
			scene.AddMesh(plane);
		}

		void Update () 
		{
		
		}

		private VisualizerMesh CreatePlane()
		{
			var factory = new GeometryFactory();
			factory.BeginMesh("Plane");
			factory.SetColor(1f, 0f, 0f, 1f);
			factory.AddVertex(0f, 0f, 0f);
			factory.AddVertex(1f, 0f, 0f);
			factory.AddVertex(1f, 1f, 0f);
			factory.AddVertex(0f, 1f, 0f);
			factory.AddTriangle(0, 1, 2);
			factory.AddTriangle(0, 2, 3);
			return factory.CompleteMesh();
		}

		private Scene scene;
	}
}