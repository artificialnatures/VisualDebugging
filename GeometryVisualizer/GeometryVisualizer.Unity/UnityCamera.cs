using UnityEngine;

namespace GeometryVisualizer.Unity
{
    internal sealed class UnityCamera : UnityNode
    {
        public void SetColor(float red, float green, float blue)
        {
            camera.backgroundColor = new Color(red, green, blue);
        }

        public void Pan(float deltaX, float deltaY)
        {
            
        }

        public void Orbit(float deltaX, float deltaY)
        {
            
        }

        public void Zoom(float delta)
        {
            
        }
        
        public UnityCamera() : base("Camera")
        {
            camera = gameObject.AddComponent<Camera>();
            camera.backgroundColor = Color.black;
            camera.clearFlags = CameraClearFlags.SolidColor;
            var pivotGameObject = new GameObject("CameraPivot");
            pivot = pivotGameObject.transform;
            zoom = 10f;
            gameObject.transform.position = new Vector3(0f, 0f, zoom);
            gameObject.transform.LookAt(pivot);
            gameObject.transform.SetParent(pivot);
        }

        private Camera camera;
        private Transform pivot;
        private float zoom;
    }
}