using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace GeometryVisualizer.Tests
{
    [Serializable]
    public class Mesh
    {
        public Vector3[] Vertices { get; }

        public int[] TriangleIndices { get; }

        public Mesh(IEnumerable<Vector3> vertices, IEnumerable<int> triangleIndices)
        {
            Vertices = vertices.ToArray();
            TriangleIndices = triangleIndices.ToArray();
        }
    }
}
