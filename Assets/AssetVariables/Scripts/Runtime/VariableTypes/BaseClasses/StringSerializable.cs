using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    public abstract class StringSerializable : ScriptableObject
    {
        public abstract string GetKey();
        public abstract string Serialize(StreamWriter streamWriter);
        public abstract void Deserialize(in string stringRepresentation);
    }
}
