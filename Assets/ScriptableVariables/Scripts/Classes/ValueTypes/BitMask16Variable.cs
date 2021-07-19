using UnityEngine;

namespace CustomLibrary.Util.ScriptableVariables
{
    [CreateAssetMenu(menuName = "Scriptable Objects/BitMasks/16")]
    public class BitMask16Variable : ScriptableObject
    {
        public delegate void ValueChangedEvent(BitMask16 newValue, BitMask16 dirtyBits);
        /// <summary>
        /// Subscribe to this Event to get notified when the value of this object changes.
        /// First Parameter: New BitMask    Second Parameter: Dirty Bits (Bits that have changed)
        /// </summary>
        public event ValueChangedEvent OnValueChanged;

        public BitMask16 Value
        {
            get => m_Value;
            set
            {
                if (!value.Equals(m_Value))
                {
                    BitMask16 dirty = ((BitMask16)m_Value).XOR(value);
                    m_Value = value;
                    OnValueChanged?.Invoke(value, dirty);
                }
            }
        }

        [SerializeField]
        private ushort m_Value;

        void Reset() => m_Value = 0;
    }
}