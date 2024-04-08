
using System;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    [Serializable]
    public struct ReadOnlyVariable<TType>
    {
        public TType Value => _value.Value;
        
        public event Action<TType, TType> OnValueChanged
        {
            add => _value.OnValueChanged += value;
            remove => _value.OnValueChanged -= value;
        }
        
        [SerializeField]
        private Variable<TType> _value;
    }
}