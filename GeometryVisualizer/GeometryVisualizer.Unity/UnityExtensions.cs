using System.Linq;
using UnityEngine;

namespace GeometryVisualizer.Unity
{
    internal static class UnityExtensions
    {
        public static Color ToUnityColor(this VisualizerColor visualizerColor)
        {
            return new Color(visualizerColor.Red, visualizerColor.Green, visualizerColor.Blue, visualizerColor.Alpha);
        }

        public static Mesh ToUnityMesh(this VisualizerMesh visualizerMesh)
        {
            var vertices = visualizerMesh.Vertices.Select(vertex => new Vector3(vertex.X, vertex.Y, vertex.Z))
                .ToArray();
            var triangleIndices = visualizerMesh.TriangleIndices.SelectMany(list => list).ToArray();
            var mesh = new Mesh();
            mesh.vertices = vertices;
            mesh.triangles = triangleIndices;
            mesh.RecalculateNormals();
            return mesh;
        }
    }
}