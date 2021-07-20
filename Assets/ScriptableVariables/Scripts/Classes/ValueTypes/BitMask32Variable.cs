using UnityEngine;

namespace CustomLibrary.Util.ScriptableVariables
{
    [CreateAssetMenu(menuName = "Scriptable Objects/BitMasks/32")]
    public class BitMask32Variable : ScriptableObject
    {
        public delegate void ValueChangedEvent(BitMask32 newMask, BitMask32 dirtyBits);
        /// <summary>
        /// Subscribe to this Event to get notified when the value of this object changes.
        /// First Parameter: New BitMask    Second Parameter: Dirty Bits (Bits that have changed)
        /// </summary>
        public event ValueChangedEvent OnValueChanged;

        /// <summary>
        /// Use this when you want to read / change the whole bit mask
        /// </summary>
        public BitMask32 Value
        {
            get => m_Value;
            set
            {
                if (!value.Equals(m_Value))
                {
                    BitMask32 dirty = ((BitMask32)m_Value).XOR(value);
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
            get => ((BitMask32)m_Value)[position];
            set
            {
                BitMask32 bm = m_Value;
                bm[position] = value;

                if (bm.Equals(m_Value))
                {
                    BitMask32 dirty = bm.XOR(m_Value);
                    m_Value = bm;
                    OnValueChanged?.Invoke(bm, dirty);
                }
            }
        }

        /// <summary>
        /// Use to broadcast a change in the mask, even though it hasn't actually changed.
        /// </summary>
        public void SetDirty(BitMask32 dirty)
            => OnValueChanged?.Invoke(m_Value, dirty);

        [SerializeField]
        private uint m_Value;

        void Reset() => m_Value = 0;
    }
}

