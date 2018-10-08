namespace GeometryVisualizer.Unity
{
    internal class UnityScene : Scene
    {
        public void SetBackgroundColor(float red, float green, float blue)
        {
            camera.SetColor(red, green, blue);
        }

        public void Pan(float deltaX, float deltaY)
        {
            camera.Pan(deltaX, deltaY);
        }

        public void Orbit(float deltaX, float deltaY)
        {
            camera.Orbit(deltaX, deltaY);
        }

        public void Zoom(float delta)
        {
            camera.Zoom(delta);
        }


        public void AddMesh(VisualizerMesh mesh)
        {
            var meshNode = new UnityMesh(mesh, rootNode, mesh.Label);
        }

        public void Reset()
        {
            throw new System.NotImplementedException();
        }
        
        public UnityScene()
        {
            rootNode = new UnityNode("Scene");
            camera = new UnityCamera();
        }

        private UnityNode rootNode;
        private UnityCamera camera;
    }
}