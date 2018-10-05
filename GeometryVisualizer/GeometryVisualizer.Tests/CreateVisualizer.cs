using Xunit;

namespace GeometryVisualizer.Tests
{
    public class CreateVisualizer
    {
        [Fact]
        public void CanCreateVisualizer()
        {
            var visualizer = Factory.CreateVisualizer();
            
            Assert.NotNull(visualizer);
        }
    }
}
