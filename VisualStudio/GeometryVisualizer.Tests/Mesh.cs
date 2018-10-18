using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Diagnostics;

namespace GeometryVisualizer.Tests
{
    [DebuggerVisualizer(typeof(Mesh))]
    [Serializable]
    public class Mesh
    {
        public Vector3[] Vertices { get; }

        public int[] TriangleIndices { get; }

        public static Mesh CreateCube()
        {
            var vertices = new[]
            {
                new Vector3(-0.5f, -0.5f, -0.5f),
                new Vector3(0.5f, -0.5f, -0.5f),
                new Vector3(0.5f, -0.5f, 0.5f),
                new Vector3(-0.5f, -0.5f, 0.5f),
                new Vector3(-0.5f, 0.5f, -0.5f),
                new Vector3(0.5f, 0.5f, -0.5f),
                new Vector3(0.5f, 0.5f, 0.5f),
                new Vector3(-0.5f, 0.5f, 0.5f)
            };
            var triangleIndices = new[]
            {
                0, 1, 2,
                0, 2, 3,
                4, 5, 1,
                4, 1, 0,
                1, 5, 6,
                1, 6, 2,
                3, 2, 6,
                3, 6, 7,
                4, 0, 3,
                4, 3, 7,
                7, 6, 5,
                7, 5, 4
            };
            return new Mesh(vertices, triangleIndices);
        }

        public Mesh(IEnumerable<Vector3> vertices, IEnumerable<int> triangleIndices)
        {
            Vertices = vertices.ToArray();
            TriangleIndices = triangleIndices.ToArray();
        }
    }
}
