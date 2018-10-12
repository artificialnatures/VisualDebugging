using UnityEngine;

namespace GeometryVisualizer.Unity
{
    internal sealed class UnityMesh : UnityNode
    {
        public UnityMesh(AssetCollection assetCollection, VisualizerMesh visualizerMesh, UnityNode parent, string label) : base(parent, label)
        {
            var renderer = gameObject.AddComponent<MeshRenderer>();
            var color = visualizerMesh.Color.ToUnityColor();
            renderer.material = assetCollection.CreateMaterial(color);
            var filter = gameObject.AddComponent<MeshFilter>();
            filter.mesh = visualizerMesh.ToUnityMesh();
        }
    }
}