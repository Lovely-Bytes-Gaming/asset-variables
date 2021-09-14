using System;
using InflamedGums.Util.Types;

namespace InflamedGums.Util.ScriptableVariables
{
    public class BitMaskType<T> : ValueType<T> where T : struct, IBitMask<T>, IEquatable<T>
    {
        public delegate void ValueChangedEvent(T newMask, T dirtyBits);
        /// <summary>
        /// Subscribe to this Event to get notified when the value of this object changes.
        /// First Parameter: New BitMask    Second Parameter: Dirty Bits (Bits that have changed)
        /// </summary>
        public new event Action<T, T> OnValueChanged;

        /// <summary>
        /// Use this when you want to read / change the whole bit mask
        /// </summary>
        public override T Value
        {
            get => base.Value;
            set
            {
                if (!value.Equals(m_Value) && !isLocked)
                {
                    T dirty = m_Value.XOR(value);
                    m_Value = value;
                    SetDirty(dirty);
                }
            }
        }

        /// <summary>
        /// Use this accessor if you want to get/set single values in the mask.
        /// </summary>
        public bool this[int position]
        {
            get => m_Value.GetAt(position);
            set
            {
                T bm = m_Value;
                bm.SetAt(position, value);

                if (!bm.Equals(m_Value) && !isLocked)
                {
                    T dirty = bm.XOR(m_Value);
                    m_Value = bm;
                    SetDirty(dirty);
                }
            }
        }

        /// <summary>
        /// Use to broadcast a change in the mask, even though it hasn't actually changed.
        /// Still works when the variable is locked, as the actual value doesn't change.
        /// </summary>
        public void SetDirty(T dirtyMask)
            => OnValueChanged?.Invoke(m_Value, dirtyMask);

        public new void Invoke()
            => SetDirty(new T().Inverse());

        public void Reset() 
            => m_Value.Reset();
    }
}