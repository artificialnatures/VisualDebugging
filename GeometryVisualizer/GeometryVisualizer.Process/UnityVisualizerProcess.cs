using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
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
            Communicator.Connect();
            Task.Delay(500).Wait();
            process = System.Diagnostics.Process.Start(startInfo);
            Task.Delay(500).Wait();
            if (Communicator.IsConnected) Console.WriteLine("Connected to visualizer.");
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
                if (File.Exists(executableMap[platformIdentifier]))
                {
                    //TODO: check version, unpack if newer version available
                }
                else
                {
                    UnpackVisualizerApp(platformIdentifier);
                }
                startInfo = GetStartInfo(platformIdentifier);
            }

            Communicator = communicator;
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

        private void UnpackVisualizerApp(string platformIdentifier)
        {
            var executableZipFile = GetExecutableZipFile(platformIdentifier);
            var appPath = AppDomain.CurrentDomain.BaseDirectory;
            var unzipPath = Path.Combine(appPath, executableDirectory);
            using (var archive = new ZipArchive(executableZipFile, ZipArchiveMode.Read))
            {
                archive.ExtractToDirectory(unzipPath);
            }
        }

        private ProcessStartInfo GetStartInfo(string platformIdentifier)
        {
            var info = new ProcessStartInfo(executableMap[platformIdentifier]);
            info.Arguments = " -nolog";
            info.WindowStyle = ProcessWindowStyle.Normal;
            return info;
        }

        private Stream GetExecutableZipFile(string platformIdentifier)
        {
            var assembly = GetType().Assembly;
            var resourceNames = assembly.GetManifestResourceNames();
            var matchingResource = resourceNames.First(rn => rn.Contains(executableDirectory) && rn.Contains(platformIdentifier) && rn.Contains("zip"));
            if (matchingResource == null) return null;
            return assembly.GetManifestResourceStream(matchingResource);
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
    }
}