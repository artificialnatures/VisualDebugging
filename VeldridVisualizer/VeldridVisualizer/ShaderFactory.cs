using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Veldrid;

namespace VeldridVisualizer
{
    public class ShaderFactory
    {
        public Shader LoadShader(GraphicsDevice graphicsDevice, ShaderStages stage)
        {
            string extension = extensions[graphicsDevice.BackendType];
            string entryPoint = stage == ShaderStages.Vertex ? "VS" : "FS";
            var shaderName = $"{stage.ToString()}.{extension}";
            return graphicsDevice.ResourceFactory.CreateShader(new ShaderDescription(stage, shaders[shaderName], entryPoint));
        }
        
        public ShaderFactory()
        {
            var assembly = Assembly.GetExecutingAssembly();
            shaders = new Dictionary<string, byte[]>();
            var resourceNames = assembly.GetManifestResourceNames().Where(resourceName => resourceName.Contains("Shaders"));
            foreach (var resourceName in resourceNames)
            {
                AddShader(assembly, resourceName);
            }
        }
        
        private void AddShader(Assembly assembly, string resourceName)
        {
            var filename = GetFilename(resourceName);
            var bytes = GetBytes(assembly, resourceName);
            shaders.Add(filename, bytes);
        }

        private string GetFilename(string resourceName)
        {
            var index = resourceName.IndexOf(filenameSeparator) + filenameSeparator.Length;
            return resourceName.Substring(index);
        }

        private byte[] GetBytes(Assembly assembly, string resourceName)
        {
            byte[] bytes;
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
            }
            return bytes;
        }

        private readonly string filenameSeparator = "Shaders.";
        private Dictionary<string, byte[]> shaders;
        private readonly Dictionary<GraphicsBackend, string> extensions = new Dictionary<GraphicsBackend, string>()
        {
            { GraphicsBackend.Direct3D11, "hlsl.bytes" },
            { GraphicsBackend.Vulkan, "spv" },
            { GraphicsBackend.OpenGL, "glsl" },
            { GraphicsBackend.Metal, "metallib" }
        };
    }
}