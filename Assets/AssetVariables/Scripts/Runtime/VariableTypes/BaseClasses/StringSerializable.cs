using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    public abstract class StringSerializable : ScriptableObject
    {
        public abstract string GetStringRepresentation();
        public abstract void InitializeFromString(in string stringRepresentation);
    }
}
