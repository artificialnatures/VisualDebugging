using System.IO;
using UnityEngine;

namespace GeometryVisualizer.Unity
{
    internal class UnityAssetCollection : AssetCollection
    {
        public Material CreateMaterial(Color color)
        {
            return new Material(defaultMaterial) { color = color };
        }

        public UnityAssetCollection()
        {
            var bundlePath = Path.Combine(Application.streamingAssetsPath, defaultAssetBundleName);
            assetBundle = AssetBundle.LoadFromFile(bundlePath);
            defaultMaterial = GetDefaultMaterial();
        }

        private Material GetDefaultMaterial()
        {
            if (assetBundle == null) return null;

            if (defaultMaterial == null) return LoadDefaultMaterial();
            return defaultMaterial;
        }

        private Material LoadDefaultMaterial()
        {
            if (assetBundle == null) return null;
            if (assetBundle.Contains(defaultMaterialName))
            {
                return assetBundle.LoadAsset<Material>(defaultMaterialName);
            }

            return null;
        }

        private AssetBundle assetBundle;
        private Material defaultMaterial;
        private readonly string defaultAssetBundleName = "geometryvisualizerassets";
        private readonly string defaultMaterialName = "DefaultMaterial";
    }
}