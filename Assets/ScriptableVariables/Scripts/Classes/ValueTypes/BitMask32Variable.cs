using System.Collections.Generic;
using UnityEngine;

namespace CustomLibrary.Util.ScriptableVariables
{
    [CreateAssetMenu(menuName = "Scriptable Objects/BitMasks/32")]
    public class BitMask32Variable : ValueType<uint>
    {
        /// <summary>
        /// Sets the bit at 'position' to 1
        /// </summary>
        public void SetBit(int position) => Value |= (uint)(1 << (position & 0x1f));
        /// <summary>
        /// Sets the bit at 'position' to 0
        /// </summary>
        public void ClearBit(int position) => Value &= (uint)~(1 << (position & 0x1f));
        /// <summary>
        /// Returns true if the bit at 'position' of the given 'bitMask' is set to 1, and false otherwise.
        /// </summary>
        public static bool IsBitSet(uint bitMask, int position) => (bitMask & (1 << (position & 0x1f))) > 0;
    }
}

