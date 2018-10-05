using SampleBase;

namespace VeldridConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var window = new VeldridStartupWindow("Geometry Visualizer");
            var app = new GeometryVisualizers.VeldridVisualizer(window);
            window.Run();
        }
    }
}
