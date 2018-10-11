using System;
using System.IO;

namespace GeometryVisualizer.Communication
{
    [Serializable]
    public class Transaction
    {
        public string PayloadType { get; }
        
        public byte[] Payload { get; }

        public Transaction(Type payloadType, Stream payload)
        {
            PayloadType = payloadType.Name;
            byte[] bytes = new byte[payload.Length];
            payload.Read(bytes, 0, bytes.Length);
            Payload = bytes;
        }
    }
}