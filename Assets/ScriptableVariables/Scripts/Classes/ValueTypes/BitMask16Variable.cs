using UnityEngine;
using System.Collections.Generic;

namespace CustomLibrary.Util.ScriptableVariables
{
    [CreateAssetMenu(menuName = "Scriptable Objects/BitMasks/16")]
    public class BitMask16Variable : ScriptableObject
    {
        public delegate void ValueChangedEvent(ushort newValue, ushort dirtyBits);
        /// <summary>
        /// Subscribe to this Event to get notified when the value of this object changes.
        /// Provides the new value as parameter.
        /// </summary>
        public event ValueChangedEvent OnValueChanged;

        public ushort Value
        {
            get => m_Value;
            set
            {
                if (!value.Equals(m_Value))
                {
                    ushort dirty = (ushort)(m_Value ^ value);
                    m_Value = value;
                    OnValueChanged?.Invoke(value, dirty);
                }
            }
        }

        [SerializeField]
        protected ushort m_Value;
        /// <summary>
        /// Sets the bit at 'position' to 1
        /// </summary>
        public void SetBit(int position) => Value |= (ushort)(1 << (position & 0xf));
        /// <summary>
        /// Sets the bit at 'position' to 0
        /// </summary>
        public void ClearBit(int position) => Value &= (ushort)~(1 << (position & 0xf));
        /// <summary>
        /// Returns true if the bit at 'position' of the given 'bitMask' is set to 1, and false otherwise.
        /// </summary>
        public static bool IsBitSet(ushort bitMask, int position) => (bitMask & (1 << (position & 0xf))) > 0;
    }
}