using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using GeometryVisualizer.Communication;

namespace GeometryVisualizer.Process
{
    internal class UnityVisualizerProcess : VisualizerProcess
    {
        public Communicator Communicator { get; }
        
        public void Start()
        {
            if (startInfo == null) return;
            process = System.Diagnostics.Process.Start(startInfo);
            var windowHandle = WaitForWindowHandle();
            if (windowHandle != nullPointer)
            {
                Communicator.Connect();
            }
            else
            {
                Console.WriteLine("Visualizer did not start.");
            }
        }

        public void Stop()
        {
            if (process == null) return;
            if (process.HasExited) return;
            Communicator.Send("quit");
            Communicator.Disconnect();
        }

        public UnityVisualizerProcess(Communicator communicator)
        {
            var platformIdentifier = GetPlatformIdentifier();
            executableMap = CreateExecutableMap();
            if (executableMap.ContainsKey(platformIdentifier))
            {
                startInfo = GetStartInfo(platformIdentifier);
            }

            Communicator = communicator;
        }

        private IntPtr WaitForWindowHandle()
        {
            var numberOfAttempts = 20;
            while (numberOfAttempts > 0)
            {
                Task.Delay(250).Wait();
                if (process.MainWindowHandle != nullPointer) return process.MainWindowHandle;
                numberOfAttempts--;
            }

            return process.MainWindowHandle;
        }

        private Dictionary<string, string> CreateExecutableMap()
        {
            return new Dictionary<string, string>
            {
                { "win32", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, executableDirectory, "win32", "GeometryVisualizer", "GeometryVisualizer.exe") },
                { "win64", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, executableDirectory, "win64", "GeometryVisualizer", "GeometryVisualizer.exe") },
                { "macOS", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, executableDirectory, "macOS", "GeometryVisualizer.app", "Contents", "MacOS", "GeometryVisualizer") }
            };
        }

        private ProcessStartInfo GetStartInfo(string platformIdentifier)
        {
            var info = new ProcessStartInfo(executableMap[platformIdentifier])
            {
                Arguments = " -nolog",
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                WindowStyle = ProcessWindowStyle.Normal
            };
            return info;
        }

        private string GetPlatformIdentifier()
        {
            var wordSize = Environment.Is64BitProcess ? "64" : "32";
            var platform = Environment.OSVersion.Platform.ToString().ToLower();
            return platform.Contains("win") ? "win" + wordSize : "macOS";
        }

        private ProcessStartInfo startInfo;

        private System.Diagnostics.Process process;
        
        private Dictionary<string, string> executableMap;

        private static readonly string executableDirectory = "GeometryVisualizer";
        
        private static readonly IntPtr nullPointer = new IntPtr();
    }
}