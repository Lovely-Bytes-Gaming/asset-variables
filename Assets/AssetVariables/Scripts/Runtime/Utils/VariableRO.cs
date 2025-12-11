
using System;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    [Serializable]
    public struct VariableRO<TVariable, TValue> 
        where TVariable : Variable<TValue>
    {
        [SerializeField]
        private TVariable _variable;

        public TValue Value => _variable.Value;
        public event Action<TValue, TValue> OnValueChanged
        {
            add => _variable.OnValueChanged += value;
            remove => _variable.OnValueChanged -= value;
        }
    }
}