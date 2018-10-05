using Xunit;

namespace GeometryVisualizer.Tests
{
    public class CreateVisualizer
    {
        [Fact]
        public void CanCreateVisualizer()
        {
            var scene = new TestScene();
            var visualizer = VisualizerFactory.CreateVisualizer(scene);
            
            Assert.NotNull(visualizer);
        }
    }
}
