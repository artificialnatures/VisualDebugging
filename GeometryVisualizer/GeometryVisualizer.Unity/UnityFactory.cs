using GeometryVisualizer.Communication;

namespace GeometryVisualizer.Unity
{
    public class UnityFactory
    {
        public Visualizer CreateVisualizer(Scene scene, Serializer serializer, Communicator communicator)
        {
            return new UnityVisualizer(scene, serializer, communicator);
        }
        
        public Scene CreateScene()
        {
            return new UnityScene(assetCollection);
        }

        public UnityFactory()
        {
            assetCollection = new UnityAssetCollection();
        }

        private AssetCollection assetCollection;
    }
}