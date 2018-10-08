using Xunit;

namespace GeometryVisualizer.Tests
{
    public class CreateVisualizer
    {
        [Fact]
        public void CanCreateVisualizer()
        {
            var scene = new TestScene();
            var factory = new VisualizerFactory();
            var visualizer = factory.CreateVisualizer(scene);
            
            Assert.NotNull(visualizer);
        }
    }
}
