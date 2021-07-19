using UnityEngine;

namespace CustomLibrary.Util.ScriptableVariables
{
    [CreateAssetMenu(menuName = "Scriptable Objects/BitMasks/32")]
    public class BitMask32Variable : ScriptableObject
    {
        public delegate void ValueChangedEvent(BitMask32 newValue, BitMask32 dirtyBits);
        /// <summary>
        /// Subscribe to this Event to get notified when the value of this object changes.
        /// First Parameter: New BitMask    Second Parameter: Dirty Bits (Bits that have changed)
        /// </summary>
        public event ValueChangedEvent OnValueChanged;

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

        [SerializeField]
        private uint m_Value;

        void Reset() => m_Value = 0;
    }
}

