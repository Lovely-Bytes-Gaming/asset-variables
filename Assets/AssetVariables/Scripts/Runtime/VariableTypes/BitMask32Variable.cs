using System;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    [CreateAssetMenu(menuName = Constants.DefaultAssetPath + nameof(BitMask32))]
    public class BitMask32Variable : ScriptableObject
    {
        /// <summary>
        /// Subscribe to this Event to get notified when the value of this object changes.
        /// Provides the old and new values as parameters.
        /// </summary>
        public event System.Action<BitMask32, BitMask32> OnValueChanged;

        public virtual BitMask32 Value
        {
            get => _value;
            set
            {
                BitMask32 oldValue = _value;
                _value = value;
                OnValueChanged?.Invoke(oldValue, _value);
            } 
        }

        public virtual void SetWithoutNotify(BitMask32 newValue)
            => _value = newValue;
        
        [SerializeField]
        private BitMask32 _value;
        /// <summary>
        /// Use this accessor if you want to get/set single values in the mask.
        /// </summary>
        public bool this[int position]
        {
            get => Value[position];
            set
            {
                BitMask32 currentMask = Value;
                currentMask[position] = value;
                Value = currentMask;
            }
        }
    }
}

