using System;
using System.IO;

namespace GeometryVisualizer.Process
{
    [Serializable]
    internal class Transaction
    {
        public string PayloadType { get; }
        
        public byte[] Payload { get; }

        public Transaction(Type payloadType, Stream payload)
        {
            PayloadType = payloadType.FullName;
            byte[] bytes = new byte[payload.Length];
            payload.Read(bytes, 0, bytes.Length);
            Payload = bytes;
        }
    }
}