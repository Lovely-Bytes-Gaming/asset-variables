using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomLibs.Util.ScriptableVariables
{
    [CreateAssetMenu(menuName = "Scriptable Objects/BitMasks/16")]
    public class BitMask16Variable : ValueType<ushort>
    {
        public void SetBit(int position) => Value |= (ushort)(1 << (position & 0xf));
        public void ClearBit(int position) => Value &= (ushort)~(1 << (position & 0xf));
        public bool GetBit(int position) => (m_Value & (1 << (position & 0xf))) > 0;
    }
}

