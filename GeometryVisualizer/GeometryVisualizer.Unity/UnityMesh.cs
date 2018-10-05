using UnityEngine;

namespace GeometryVisualizer.Unity
{
    internal class UnityMesh : UnityNode
    {
        public UnityMesh(VisualizerMesh visualizerMesh, UnityNode parent, string label) : base(parent, label)
        {
            var renderer = gameObject.AddComponent<MeshRenderer>();
            renderer.material = CreateMaterial(visualizerMesh);
            var filter = gameObject.AddComponent<MeshFilter>();
            filter.mesh = visualizerMesh.ToUnityMesh();
        }

        private static Material CreateMaterial(VisualizerMesh visualizerMesh)
        {
            var material = new Material(Shader.Find("Standard"));
            material.color = visualizerMesh.Color.ToUnityColor();
            return material;
        }
    }
}