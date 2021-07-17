using UnityEngine;
using System.Collections.Generic;

namespace CustomLibrary.Util.ScriptableVariables
{
    public abstract class BitMaskType<T> : ScriptableObject where T : IBitMask
    {
        public delegate void ValueChangedEvent(T newValue, T dirtyBits);
        /// <summary>
        /// Subscribe to this Event to get notified when the value of this object changes.
        /// First Parameter: New BitMask    Second Parameter: Dirty Bits (Bits that have changed)
        /// </summary>
        public event ValueChangedEvent OnValueChanged;

        public T Value
        {
            get => m_Value;
            set
            {
                if (!value.Equals(m_Value))
                {
                    T dirty = XOR(m_Value, value);
                    m_Value = value;
                    OnValueChanged?.Invoke(value, dirty);
                }
            }
        }

        // implementation specific. Needed to identify the dirty bits after a change.
        protected abstract T XOR(T a, T b);

        [SerializeField]
        protected T m_Value;
    }
}