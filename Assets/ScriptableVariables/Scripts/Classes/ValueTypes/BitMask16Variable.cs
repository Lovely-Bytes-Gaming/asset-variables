using UnityEngine;
using System;

namespace InflamedGums.Util.ScriptableVariables
{
    [CreateAssetMenu(menuName = "Scriptable Objects/BitMasks/16")]
    public class BitMask16Variable : ScriptableObject
    {
        public delegate void ValueChangedEvent(BitMask16 newMask, BitMask16 dirtyBits);
        /// <summary>
        /// Subscribe to this Event to get notified when the value of this object changes.
        /// First Parameter: New BitMask    Second Parameter: Dirty Bits (Bits that have changed)
        /// </summary>
        public event ValueChangedEvent OnValueChanged;
        /// <summary>
        /// You can optionally define a function here that checks whether this value can be edited.
        /// </summary>
        public bool isLocked =  false;

        /// <summary>
        /// Use this when you want to read / change the whole bit mask
        /// </summary>
        public BitMask16 Value
        {
            get => m_Value;
            set
            {
                if (!value.Equals(m_Value) && !isLocked)
                {
                    BitMask16 dirty = ((BitMask16)m_Value).XOR(value);
                    m_Value = value;
                    OnValueChanged?.Invoke(value, dirty);
                }
            }
        }

        /// <summary>
        /// Use this accessor if you want to get/set single values in the mask.
        /// </summary>
        public bool this[int position]
        {
            get => ((BitMask16)m_Value)[position];
            set
            {
                BitMask16 bm = m_Value;
                bm[position] = value;

                if(!bm.Equals(m_Value) && !isLocked)
                {
                    BitMask16 dirty = bm.XOR(m_Value);
                    m_Value = bm;
                    OnValueChanged?.Invoke(bm, dirty);
                }
            }
        }

        /// <summary>
        /// Use to broadcast a change in the mask, even though it hasn't actually changed.
        /// Still works when the variable is locked, as the actual value doesn't change.
        /// </summary>
        public void SetDirty(BitMask16 dirtyMask)
            => OnValueChanged?.Invoke(m_Value, dirtyMask);

        [SerializeField]
        private ushort m_Value;

        public void Reset() => Value = 0;
    }
}