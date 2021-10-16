using System;
using System.Collections;
using System.Collections.Generic;

namespace InflamedGums.Util.Types
{
    public interface IBitMask<T> where T : struct, IEquatable<T>
    {
        /// <summary>
        /// Logical NOT: returns a copy of this mask with flipped bits (0 -> 1 and vice versa)
        /// </summary>
        T Inverse();
        /// <summary>
        /// returns a copy of this mask with bits shifted to the left, inserting 0 from the right
        /// </summary>
        T ShiftLeft(int count);
        /// <summary>
        /// returns a copy of this mask with bits shifted to the right, inserting 0 from the left
        /// </summary>
        T ShiftRight(int count);
        /// <summary>
        /// Logical AND: returns a mask with all bits set to 1 where both masks have a value of 1.
        /// Sets the rest to 0.
        /// </summary>
        T AND(T other);
        /// <summary>
        /// Logical OR: returns a mask with all bits set to 1 where either mask has a value of 1.
        /// Sets the rest to 0.
        /// </summary>
        T OR(T other);
        /// <summary>
        /// Logical XOR: returns a bit mask with all bits set to 1 where the masks have different values.
        /// Sets the rest to 0.
        /// </summary>
        T XOR(T other);
        bool GetAt(int position);
        void SetAt(int position, bool value);
        void Reset();
    }

    /// <summary>
    /// Represents a mask with 8 bits.
    /// Single bits can be accessed like an array.
    /// Uses a byte as backing value
    /// and can be implicitly converted from and to byte.
    /// </summary>
    public struct BitMask8
        : IEquatable<BitMask8>,
        IEnumerable<bool>,
        IBitMask<BitMask8>
    {
        private byte value;

        public static implicit operator BitMask8(byte val)
            => new BitMask8 { value = val };

        public static implicit operator byte(BitMask8 bm)
            => bm.value;

        public override string ToString()
            => Convert.ToString(value, 2);

        public BitMask8(byte value)
            => this.value = value;

        public bool Equals(BitMask8 other)
            => value == other.value;
        public BitMask8 Inverse()
            => (byte)~value;
        public BitMask8 ShiftLeft(int count)
            => (byte)(value << count);
        public BitMask8 ShiftRight(int count)
            => value = (byte)(value >> count);
        public BitMask8 AND(BitMask8 other)
            => (byte)(value & other);
        public BitMask8 OR(BitMask8 other)
            => (byte)(value | other);
        public BitMask8 XOR(BitMask8 other)
            => (byte)(value ^ other);

        /// <summary>
        /// array-like getter/setter for individual bit positions.
        /// </summary>
        public bool this[int position]
        {
            get => (this & 1 << position) > 0;
            set => this = value ? (byte)(this | (1 << position))
                : (byte)(this & ~(1 << position));
        }

        public bool GetAt(int position)
            => this[position];

        public void SetAt(int position, bool value)
            => this[position] = value;

        public void Reset()
            => value = 0;

        public IEnumerator<bool> GetEnumerator()
        {
            for (int i = 0; i < 8; ++i)
                yield return this[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }

    /// <summary>
    /// Represents a mask with 16 bits.
    /// Single bits can be accessed like an array.
    /// Uses a ushort as backing value
    /// and can be implicitly converted from and to ushort.
    /// </summary>
    public struct BitMask16 
        : IEquatable<BitMask16>, 
        IEnumerable<bool>, 
        IBitMask<BitMask16>
    {
        private ushort value;

        public static implicit operator BitMask16(ushort val)
            => new BitMask16 { value = val };

        public static implicit operator ushort(BitMask16 bm)
            => bm.value;

        public override string ToString()
            => Convert.ToString(value, 2);

        public BitMask16(ushort value)
            => this.value = value;
        
        public bool Equals(BitMask16 other)
            => value == other.value;
        public BitMask16 Inverse()
            => (ushort)~value;
        public BitMask16 ShiftLeft(int count)
            => (ushort)(value << count);
        public BitMask16 ShiftRight(int count)
            => value = (ushort)(value >> count);
        public BitMask16 AND(BitMask16 other)
            => (ushort)(value & other);
        public BitMask16 OR(BitMask16 other)
            => (ushort)(value | other);
        public BitMask16 XOR(BitMask16 other)
            => (ushort)(value ^ other);
        
        /// <summary>
        /// array-like getter/setter for individual bit positions.
        /// </summary>
        public bool this[int position]
        {
            get => (this & 1 << position) > 0;
            set => this = value ? (ushort)(this | (1 << position))
                : (ushort)(this & ~(1 << position));
        }

        public bool GetAt(int position)
            => this[position];

        public void SetAt(int position, bool value)
            => this[position] = value;

        public void Reset()
            => value = 0;

        public IEnumerator<bool> GetEnumerator()
        {
            for (int i = 0; i < 16; ++i)
                yield return this[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }

    /// <summary>
    /// Represents a mask with 32 bits.
    /// Single bits can be accessed like an array.
    /// Uses a uint as backing value
    /// and can be implicitly converted from and to uint.
    /// </summary>
    public struct BitMask32 
        : IEquatable<BitMask32>, 
        IEnumerable<bool>, 
        IBitMask<BitMask32>
    {
        private uint value;

        public static implicit operator BitMask32(uint val)
            => new BitMask32 { value = val };

        public static implicit operator uint(BitMask32 bm)
            => bm.value;

        public BitMask32(uint value)
            => this.value = value;

        public static BitMask32 operator ~(BitMask32 bm)
        => ~bm.value;

        public static BitMask32 operator ^(BitMask32 a, BitMask32 b)
            => (a.value ^ b.value);

        public override string ToString()
            => Convert.ToString(value, 2);

        public bool Equals(BitMask32 other)
        => value == other.value;
        public BitMask32 Inverse()
            => ~value;
        public BitMask32 ShiftLeft(int count)
            => value << count;
        public BitMask32 ShiftRight(int count)
            => value >> count;
        public BitMask32 AND(BitMask32 other)
            => value & other;
        public BitMask32 OR(BitMask32 other)
            => value | other;
        public BitMask32 XOR(BitMask32 other)
            => value ^ other;

        /// <summary>
        /// array-like getter/setter for individual bit positions.
        /// </summary>
        public bool this[int i]
        {
            get => (this & 1 << i) > 0;
            set => this = value ? this | (uint)(1 << i)
                : this & ~(uint)(1 << i);
        }

        public bool GetAt(int position)
            => this[position];

        public void SetAt(int position, bool value)
            => this[position] = value;

        public void Reset()
            => value = 0;

        public IEnumerator<bool> GetEnumerator()
        {
            for (int i = 0; i < 32; ++i)
                yield return this[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}
