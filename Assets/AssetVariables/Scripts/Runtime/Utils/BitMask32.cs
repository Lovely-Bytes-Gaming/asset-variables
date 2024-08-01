using System;
using System.Collections.Generic;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    /// <summary>
    /// Represents a mask with 32 bits.
    /// Single bits can be accessed like an array.
    /// Uses an int as backing value
    /// and can be implicitly converted from and to int.
    /// </summary>
    [Serializable]
    public struct BitMask32 
    {
        [SerializeField]
        private int _value;

        public static implicit operator BitMask32(int val)
            => new BitMask32 { _value = val };

        public static implicit operator int(BitMask32 bm)
            => bm._value;
        
        public BitMask32(int value)
            => _value = value;
        
        public static BitMask32 FromList(List<bool> values, int startIndex, int endIndex)
        {
            BitMask32 result = 0;

            for (int i = startIndex; i < endIndex; ++i)
                result[i] = values[i];

            return result;
        }
        
        public void IntoList(List<bool> target, int startIndex, int endIndex)
        {
            for (int i = startIndex; i < endIndex; ++i)
                target[i] = this[i];
        }

        public static BitMask32 FromList(List<bool> values)
            => FromList(values, 0, values.Count);

        public void IntoList(List<bool> target)
            => IntoList(target, 0, target.Count);
        
        public static BitMask32 FromArray(bool[] values, int startIndex, int endIndex)
        {
            BitMask32 result = 0;

            for (int i = startIndex; i < endIndex; ++i)
                result[i] = values[i];

            return result;
        }

        public void IntoArray(bool[] target, int startIndex, int endIndex)
        {
            for (int i = startIndex; i < endIndex; ++i)
                target[i] = this[i];
        }

        public static BitMask32 FromArray(bool[] values)
            => FromArray(values, 0, values.Length);

        public void IntoArray(bool[] target)
            => IntoArray(target, 0, target.Length);

        public static BitMask32 FromVariables(List<BoolVariable> values, int startIndex, int endIndex)
        {
            BitMask32 result = 0;

            for (int i = startIndex; i < endIndex; ++i)
                result[i] = values[i].Value;

            return result;
        }
        
        public void IntoVariables(List<BoolVariable> target, int startIndex, int endIndex)
        {
            for (int i = startIndex; i < endIndex; ++i)
                target[i].Value = this[i];
        }

        public static BitMask32 FromVariables(List<BoolVariable> values)
            => FromVariables(values, 0, values.Count);

        public void IntoVariables(List<BoolVariable> target)
            => IntoVariables(target, 0, target.Count);
        
        public static BitMask32 operator ~(BitMask32 bm)
        => ~bm._value;

        public static BitMask32 operator ^(BitMask32 a, BitMask32 b)
            => (a._value ^ b._value);

        public override string ToString()
            => System.Convert.ToString(_value, 2);

        public bool Equals(BitMask32 other)
        => _value == other._value;
        
        public BitMask32 Inverse()
            => ~_value;
        
        public BitMask32 ShiftLeft(int count)
            => _value << count;
        
        public BitMask32 ShiftRight(int count)
            => _value >> count;
        
        public BitMask32 And(BitMask32 other)
            => _value & other;
        
        public BitMask32 Or(BitMask32 other)
            => _value | other;
        
        public BitMask32 Xor(BitMask32 other)
            => _value ^ other;

        /// <summary>
        /// array-like getter/setter for individual bit positions.
        /// </summary>
        public bool this[int i]
        {
            get => (this & 1 << i) > 0;
            set => this = value ? this | (1 << i)
                : this & ~(1 << i);
        }
        public void Reset()
            => _value = 0;
    }
}
