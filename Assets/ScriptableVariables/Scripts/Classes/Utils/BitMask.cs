using System;
using System.Collections;
using System.Collections.Generic;


namespace InflamedGums.Util.ScriptableVariables
{
    /// <summary>
    /// Represents a mask with 16 bits.
    /// Single bits can be accessed like an array.
    /// Uses a ushort as backing value
    /// and can be implicitly converted from and to ushort.
    /// </summary>
    public struct BitMask16 : IEquatable<BitMask16>, IEnumerable<bool>
    {
        private ushort value;

        public static implicit operator BitMask16(ushort val)
            => new BitMask16 { value = val };

        public static implicit operator ushort(BitMask16 bm)
            => bm.value;

        public override string ToString()
            => Convert.ToString(value, 2);

        public BitMask16(ushort value)
        {
            this.value = value;
        }

        public bool Equals(BitMask16 other)
            => value == other.value;
        /// <summary>
        /// Logical NOT: returns a copy of this mask with flipped bits (0 -> 1 and vice versa)
        /// </summary>
        public BitMask16 Inverse()
            => (ushort)~value;
        /// <summary>
        /// returns a copy of this mask with bits shifted to the left, inserting 0 from the right
        /// </summary>
        public BitMask16 ShiftLeft(int count)
            => (ushort)(value << count);
        /// <summary>
        /// returns a copy of this mask with bits shifted to the right, inserting 0 from the left
        /// </summary>
        public BitMask16 ShiftRight(int count)
            => value = (ushort)(value >> count);
        /// <summary>
        /// Logical AND: returns a mask with all bits set to 1 where both masks have a value of 1.
        /// Sets the rest to 0.
        /// </summary>
        public BitMask16 AND(BitMask16 other)
            => (ushort)(value & other);
        /// <summary>
        /// Logical OR: returns a mask with all bits set to 1 where either mask has a value of 1.
        /// Sets the rest to 0.
        /// </summary>
        public BitMask16 OR(BitMask16 other)
            => (ushort)(value | other);
        /// <summary>
        /// Logical XOR: returns a bit mask with all bits set to 1 where the masks have different values.
        /// Sets the rest to 0.
        /// </summary>
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
    public struct BitMask32 : IEquatable<BitMask32>, IEnumerable<bool>
    {
        private uint value;

        public static implicit operator BitMask32(uint val)
            => new BitMask32 { value = val };

        public static implicit operator uint(BitMask32 bm)
            => bm.value;

        public BitMask32(uint value)
        {
            this.value = value;
        }

        public static BitMask32 operator ~(BitMask32 bm)
        => ~bm.value;

        public static BitMask32 operator ^(BitMask32 a, BitMask32 b)
            => (a.value ^ b.value);

        public override string ToString()
            => Convert.ToString(value, 2);

        public bool Equals(BitMask32 other)
        => value == other.value;
        /// <summary>
        /// Logical NOT: returns a copy of this mask with flipped bits (0 -> 1 and vice versa)
        /// </summary>
        public BitMask32 Inverse()
            => ~value;
        /// <summary>
        /// returns a copy of this mask with bits shifted to the left, inserting 0 from the right
        /// </summary>
        public BitMask32 ShiftLeft(int count)
            => value << count;
        /// <summary>
        /// returns a copy of this mask with bits shifted to the right, inserting 0 from the left
        /// </summary>
        public BitMask32 ShiftRight(int count)
            => value >> count;
        /// <summary>
        /// Logical AND: returns a mask with all bits set to 1 where both masks have a value of 1.
        /// Sets the rest to 0.
        /// </summary>
        public BitMask32 AND(BitMask32 other)
            => value & other;
        /// <summary>
        /// Logical OR: returns a mask with all bits set to 1 where either mask has a value of 1.
        /// Sets the rest to 0.
        /// </summary>
        public BitMask32 OR(BitMask32 other)
            => value | other;
        /// <summary>
        /// Logical XOR: returns a bit mask with all bits set to 1 where the masks have different values.
        /// Sets the rest to 0.
        /// </summary>
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

        public IEnumerator<bool> GetEnumerator()
        {
            for (int i = 0; i < 32; ++i)
                yield return this[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}
