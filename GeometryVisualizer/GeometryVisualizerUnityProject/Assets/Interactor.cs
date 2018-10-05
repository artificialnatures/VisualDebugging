using UnityEngine;

namespace GeometryVisualizer.Unity
{
	public class Interactor : MonoBehaviour
	{
		void Start()
		{
			bootstrapper = gameObject.GetComponent<Bootstrapper>();
			previousMousePosition = Input.mousePosition;
		}

		void Update()
		{
			ProcessMouseEvents();
		}

		void ProcessMouseEvents()
		{
			var mousePosition = Input.mousePosition;
			var mouseVelocity = mousePosition - previousMousePosition;
			bootstrapper?.ReportMouseMovement(mouseVelocity);
			previousMousePosition = mousePosition;
		}

		private Bootstrapper bootstrapper;
		private Vector3 previousMousePosition;
	}
}