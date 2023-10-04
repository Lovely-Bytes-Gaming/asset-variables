using System;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    [Serializable]
    public class Observable<TValue>
    {
        public TValue Value
        {
            get => _value;
            set
            {
                TValue oldValue = _value;
                _value = value;
                
                OnValueChanged?.Invoke(oldValue, _value);
            }
        }

        public event Action<TValue, TValue> OnValueChanged;

        public Observable(TValue initialValue)
        {
            _value = initialValue;
        }
        
        [SerializeField, GetSet(nameof(Value))] 
        private TValue _value;
    }
}
