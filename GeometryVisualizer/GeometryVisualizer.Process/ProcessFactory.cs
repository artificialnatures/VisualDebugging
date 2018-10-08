namespace GeometryVisualizer.Process
{
    public class ProcessFactory
    {
        public VisualizerProcess CreateVisualizerProcess(VisualizerType visualizerType)
        {
            if (visualizerType == VisualizerType.Unity) return new UnityVisualizerProcess();
            return null;
        }
    }
}