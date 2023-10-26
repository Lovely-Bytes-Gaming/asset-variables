using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    public interface IReadOnlyWrapper<out TType> 
    {
        public TType Value { get; }
        event System.Action<TType, TType> OnValueChanged;
    }
}
