using System.Collections.Generic;
using UnityEngine;

namespace GeometryVisualizer.Unity
{
    internal class UnityNode
    {
        public Transform Transform => gameObject.transform;

        public void Reset()
        {
            children.ForEach(child => child.Destroy());
            children.Clear();
        }

        public void Destroy()
        {
            GameObject.Destroy(gameObject);
        }
        
        public UnityNode(UnityNode parent, string label) : this(label)
        {
            gameObject.transform.SetParent(parent.Transform);
        }
        
        public UnityNode(string label)
        {
            gameObject = new GameObject(label);
            children = new List<UnityNode>();
        }

        protected GameObject gameObject;
        protected List<UnityNode> children;
    }
}