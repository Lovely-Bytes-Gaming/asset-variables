using System;

namespace LovelyBytes.AssetVariables
{
    [Serializable]
    public struct DefaultValue<TType>
    {
        public bool Use;
        public TType Value;
    }
}