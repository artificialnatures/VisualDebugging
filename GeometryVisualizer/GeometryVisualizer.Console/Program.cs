﻿using GeometryVisualizer.Process;

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
                var input = System.Console.ReadLine();
                if (input == null) continue;
                input = input.ToLower();
                if (input.StartsWith("q")) Quit();
                if (input.StartsWith("h")) PrintHelp();
                if (input.StartsWith("p")) SendPlane();
                if (input.StartsWith("m")) BeginMesh();
                if (input.StartsWith("s")) SendMesh();
                if (input.StartsWith("v")) AddVertex(Parse(input));
                if (input.StartsWith("t")) AddTriangle(Parse(input));
                if (input.StartsWith("c")) SetColor(Parse(input));
                if (input.StartsWith("r")) Reconnect();
            }
        }

        static void Start()
        {
            var processFactory = new ProcessFactory();
            process = processFactory.CreateVisualizerProcess(VisualizerType.Unity);
            process.Start();
            geometryFactory = new GeometryFactory();
        }

        static string[] Parse(string input)
        {
            return input.Split(" ");
        }

        static void BeginMesh()
        {
            geometryFactory.BeginMesh("Mesh");
        }

        static void SetColor(string[] components)
        {
            if (components.Length != 5) return;
            float.TryParse(components[1], out var r);
            float.TryParse(components[2], out var g);
            float.TryParse(components[3], out var b);
            float.TryParse(components[4], out var a);
            geometryFactory.SetColor(r, g, b, a);
        }

        static void AddVertex(string[] components)
        {
            if (components.Length != 4) return;
            float.TryParse(components[1], out var x);
            float.TryParse(components[2], out var y);
            float.TryParse(components[3], out var z);
            geometryFactory.AddVertex(x, y, z);
        }

        static void AddTriangle(string[] components)
        {
            if (components.Length != 4) return;
            int.TryParse(components[1], out var i0);
            int.TryParse(components[2], out var i1);
            int.TryParse(components[3], out var i2);
            geometryFactory.AddTriangle(i0, i1, i2);
        }

        static void SendMesh()
        {
            var mesh = geometryFactory.CompleteMesh();
            process.Communicator.Send(mesh);
        }

        static void SendPlane()
        {
            geometryFactory.BeginMesh("Plane");
            geometryFactory.SetColor(1.0f, 0.0f, 0.0f, 1.0f);
            geometryFactory.AddVertex(-0.5f, -0.5f, 0.0f);
            geometryFactory.AddVertex(0.5f, -0.5f, 0.0f);
            geometryFactory.AddVertex(0.5f, 0.5f, 0.0f);
            geometryFactory.AddVertex(-0.5f, 0.5f, 0.0f);
            geometryFactory.AddTriangle(0, 1, 2);
            geometryFactory.AddTriangle(0, 2, 3);
            var mesh = geometryFactory.CompleteMesh();
            process.Communicator.Send(mesh);
        }

        static void PrintHelp()
        {
            System.Console.WriteLine("GeometryVisualizer");
            System.Console.WriteLine("-----------------------------------------------");
            if (process.Communicator.IsConnected)
            {
                System.Console.WriteLine("Connected to visualizer");
            }
            else
            {
                System.Console.WriteLine("Could not connect to visualizer");
            }
            System.Console.WriteLine("-----------------------------------------------");
            System.Console.WriteLine("Commands:");
            System.Console.WriteLine("h[elp] - show this list");
            System.Console.WriteLine("q[uit] - exits program");
            System.Console.WriteLine("p[lane] - send a plane to the visualizer");
            System.Console.WriteLine("m[esh] - start entering mesh data");
            System.Console.WriteLine("c[olor] r g b a - set mesh color");
            System.Console.WriteLine("v[ertex] x y z - add a vertex");
            System.Console.WriteLine("t[riangle] 0 1 2 - a a triangle");
            System.Console.WriteLine("s[end] - sends the geometetry to the visualizer");
            System.Console.WriteLine("r[econnect] - attempt to reconnect to the visualizer");
        }

        static void Reconnect()
        {
            if (process.Communicator.IsConnected) process.Communicator.Disconnect();
            process.Communicator.Connect();
        }

        static void Quit()
        {
            isRunning = false;
            process.Stop();
            System.Console.WriteLine("Geometry Visualizer exited.");
        }

        private static bool isRunning = true;
        private static VisualizerProcess process;
        private static GeometryFactory geometryFactory;
    }
}
