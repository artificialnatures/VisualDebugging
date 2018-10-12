using System.Collections.Generic;

namespace GeometryVisualizer.Unity
{
    internal class UnityScene : Scene
    {
        public IEnumerable<VisualizerNode> Nodes => nodes;
        
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
            var meshNode = new UnityMesh(assetCollection, mesh, rootNode, mesh.Label);
            nodes.Add(meshNode);
        }

        public void Reset()
        {
            throw new System.NotImplementedException();
        }
        
        public UnityScene(AssetCollection assetCollection)
        {
            this.assetCollection = assetCollection;
            rootNode = new UnityNode("Scene");
            nodes = new List<VisualizerNode>();
            camera = new UnityCamera();
        }

        private AssetCollection assetCollection;
        private UnityNode rootNode;
        private List<VisualizerNode> nodes;
        private UnityCamera camera;
    }
}