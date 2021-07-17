using UnityEngine;

namespace CustomLibrary.Util.ScriptableVariables
{
    [CreateAssetMenu(menuName = "Scriptable Objects/BitMasks/16")]
    public class BitMask16Variable : BitMaskType<BitMask16>
    {
        protected override BitMask16 XOR(BitMask16 a, BitMask16 b)
            => a ^ b;

        void Reset() => m_Value = 0;
    }
}