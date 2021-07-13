using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomLibs.Util.ScriptableVariables
{
    [CreateAssetMenu(menuName = "Scriptable Objects/BitMasks/32")]
    public class BitMask32Variable : ValueType<uint>
    {
        public void SetBit(int position) => Value |= (uint)(1 << (position & 0x1f));
        public void ClearBit(int position) => Value &= (uint)~(1 << (position & 0x1f));
        public bool GetBit(int position) => (m_Value & (1 << (position & 0x1f))) > 0;
    }
}

