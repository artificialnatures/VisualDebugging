using GeometryVisualizer.Process;

namespace GeometryVisualizer.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Geometry Visualizer");
            Start();
            while (isRunning)
            {
                var input = System.Console.ReadLine().ToLower();
                if (input.Contains("q")) Quit();
                if (input.Contains("h")) PrintHelp();
            }
        }

        static void Start()
        {
            var factory = new ProcessFactory();
            process = factory.CreateVisualizerProcess(VisualizerType.Unity);
            process.Start();
        }

        static void PrintHelp()
        {
            System.Console.WriteLine("Commands:");
            System.Console.WriteLine("h[elp] - show this list");
            System.Console.WriteLine("q[uit] - exits program");
        }

        static void Quit()
        {
            isRunning = false;
            process.Stop();
            System.Console.WriteLine("Geometry Visualizer exited.");
        }

        private static bool isRunning = true;
        private static VisualizerProcess process;
    }
}
