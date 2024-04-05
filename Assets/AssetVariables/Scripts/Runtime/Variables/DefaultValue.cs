
using System;
using UnityEngine.Serialization;

namespace LovelyBytes.AssetVariables
{
    [Serializable]
    public class DefaultValue<TType>
    {
        public bool Use;
        public TType Value;
    }
}