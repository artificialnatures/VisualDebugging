using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Xunit;

namespace GeometryVisualizer.Tests
{
    public class CreateMesh
    {
        [Fact]
        public void CanCreateMesh()
        {
            var label = "Mesh123";
            var color = new[] { 0.2f, 0.4f, 0.6f, 1f };
            var vertices = new[]
            {
                new[] { 0f, 0f, 0f },
                new[] { 1f, 0f, 0f },
                new[] { 1f, 1f, 0f },
                new[] { 0f, 1f, 0f }
            };
            var indices = new[]
            {
                new[] { 0, 1, 2 },
                new[] { 0, 2, 3 }
            };
            
            var factory = new GeometryFactory();
            factory.BeginMesh(label);
            factory.SetColor(color[0], color[1], color[2], color[3]);
            foreach (var vertex in vertices)
            {
                factory.AddVertex(vertex[0], vertex[1], vertex[2]);
            }
            foreach (var triangle in indices)
            {
                factory.AddTriangle(triangle[0], triangle[1], triangle[2]);
            }
            var mesh = factory.CompleteMesh();
            
            Assert.Equal(label, mesh.Label);
            Assert.True(AreEqual(color, mesh.Color));
            var allVerticesAreEqual = mesh.Vertices.Zip(vertices,
                (vector, array) => AreEqual(array, vector)).All(b => b);
            Assert.True(allVerticesAreEqual);
            var allIndicesAreEqual = mesh.TriangleIndices.Zip(indices, 
                (list, array) => AreEqual(array, list)).All(b => b);
            Assert.True(allIndicesAreEqual);
        }

        private bool AreEqual(float[] expected, VisualizerColor actual)
        {
            return AreEqual(expected[0], actual.Red) &&
                   AreEqual(expected[1], actual.Green) &&
                   AreEqual(expected[2], actual.Blue) &&
                   AreEqual(expected[3], actual.Alpha);
        }

        private bool AreEqual(float[] vertexExpected, VisualizerVertex vertexActual)
        {
            return AreEqual(vertexExpected[0], vertexActual.X) && 
                   AreEqual(vertexExpected[1], vertexActual.Y) &&
                   AreEqual(vertexExpected[2], vertexActual.Z);
        }

        private bool AreEqual(int[] indicesExpected, IEnumerable<int> indicesActual)
        {
            var actualArray = indicesActual.ToArray();
            return indicesExpected[0] == actualArray[0] &&
                   indicesExpected[1] == actualArray[1] &&
                   indicesExpected[2] == actualArray[2];
        }

        private bool AreEqual(float expected, float actual)
        {
            return Math.Abs(expected - actual) < float.Epsilon;
        }
    }
}