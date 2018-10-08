using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace GeometryVisualizer.Process
{
    internal class UnityVisualizerProcess : VisualizerProcess
    {
        public void Start()
        {
            if (startInfo == null) return;
            process = System.Diagnostics.Process.Start(startInfo);
        }

        public void Stop()
        {
            process?.Kill();
        }

        public UnityVisualizerProcess()
        {
            var platformIdentifier = GetPlatformIdentifier();
            if (executableMap.ContainsKey(platformIdentifier))
            {
                if (!File.Exists(executableMap[platformIdentifier]))
                {
                    UnpackVisualizerApp(platformIdentifier);
                }
                startInfo = GetStartInfo(platformIdentifier);
            }
        }

        private void UnpackVisualizerApp(string platformIdentifier)
        {
            var executableZipFile = GetExecutableZipFile(platformIdentifier);
            var appPath = AppDomain.CurrentDomain.BaseDirectory;
            var unzipPath = Path.Combine(appPath, "UnityVisualizer");
            using (var archive = new ZipArchive(executableZipFile, ZipArchiveMode.Read))
            {
                archive.ExtractToDirectory(unzipPath);
            }
        }

        private ProcessStartInfo GetStartInfo(string platformIdentifier)
        {
            var startInfo = new ProcessStartInfo(executableMap[platformIdentifier]);
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            return startInfo;
        }

        private Stream GetExecutableZipFile(string platformIdentifier)
        {
            var assembly = GetType().Assembly;
            var resourceNames = assembly.GetManifestResourceNames();
            var matchingResource = resourceNames.First(rn => rn.Contains("UnityVisualizer") && rn.Contains(platformIdentifier) && rn.Contains("zip"));
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
        
        private static readonly Dictionary<string, string> executableMap = new Dictionary<string, string>
        {
            { "win", Path.Combine("UnityVisualizer", "GeometryVisualizer", "GeometryVisualizer.exe") },
            { "mac", Path.Combine("UnityVisualizer", "GeometryVisualizer.app", "Contents", "MacOS", "GeometryVisualizer") }
        };
    }
}