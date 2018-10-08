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
		}

		void Update () 
		{
		
		}

		private Scene scene;
	}
}