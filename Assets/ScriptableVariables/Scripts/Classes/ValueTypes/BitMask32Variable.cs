using UnityEngine;

namespace CustomLibrary.Util.ScriptableVariables
{
    [CreateAssetMenu(menuName = "Scriptable Objects/BitMasks/32")]
    public class BitMask32Variable : BitMaskType<BitMask32>
    {
        protected override BitMask32 XOR(BitMask32 a, BitMask32 b)
            => a ^ b;

        void Reset() => m_Value = 0;
    }
}

