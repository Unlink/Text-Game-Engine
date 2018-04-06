using System;
using System.Runtime.Serialization;
using GameFramework;

namespace Hra.rooms
{
    [Serializable]
    internal class Rozcestie : Room
    {
        public Rozcestie(string name) : base(name)
        {
        }

        public Rozcestie(SerializationInfo info, StreamingContext ctxt) : base(info, ctxt)
        {
        }
    }
}