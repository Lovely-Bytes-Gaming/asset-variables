using System;

/// <summary>
/// Interface describing a bitmasks
/// </summary>
public interface IBitMask 
{
    /// <summary>
    /// Logical NOT: flips all bits (0 -> 1 and vice versa)
    /// </summary>
    public void Invert();
    /// <summary>
    /// Shift bits to the left, inserting 0 from the right
    /// </summary>
    public void ShiftLeft(int count);
    /// <summary>
    /// Shift bits to the right, inserting 0 from the left
    /// </summary>
    public void ShiftRight(int count);


    /// <summary>
    /// Logical AND: returns bit masks with all bits set to 1 where both masks have a value of 1.
    /// Sets the rest to 0.
    /// The type of the returned bitmask is the type of the input operand 'other'.
    /// </summary>
    public BitMask16 AND(BitMask16 other);
    /// <summary>
    /// Logical OR: returns bit masks with all bits set to 1 where either mask has a value of 1.
    /// Sets the rest to 0.
    /// The type of the returned bitmask is the type of the input operand 'other'.
    /// </summary>
    public BitMask16 OR(BitMask16 other);
    /// <summary>
    /// Logical XOR: returns bit masks with all bits set to 1 where the masks have different values.
    /// Sets the rest to 0.
    /// The type of the returned bitmask is the type of the input operand 'other'.
    /// </summary>
    public BitMask16 XOR(BitMask16 other);
    /// <summary>
    /// Logical AND: returns bit masks with all bits set to 1 where both masks have a value of 1.
    /// Sets the rest to 0.
    /// The type of the returned bitmask is the type of the input operand 'other'.
    /// </summary>
    public BitMask32 AND(BitMask32 other);
    /// <summary>
    /// Logical OR: returns bit masks with all bits set to 1 where either mask has a value of 1.
    /// Sets the rest to 0.
    /// The type of the returned bitmask is the type of the input operand 'other'.
    /// </summary>
    public BitMask32 OR(BitMask32 other);
    /// <summary>
    /// Logical XOR: returns bit masks with all bits set to 1 where the masks have different values.
    /// Sets the rest to 0.
    /// The type of the returned bitmask is the type of the input operand 'other'.
    /// </summary>
    public BitMask32 XOR(BitMask32 other);

}

/// <summary>
/// Represents a mask with 16 bits.
/// Single bits can be accessed like an array.
/// Uses a ushort as backing value
/// and can implicitly converted from and to ushort.
/// </summary>
public struct BitMask16 : IEquatable<BitMask16>, IBitMask
{
    private ushort value;

    public static implicit operator BitMask16(ushort val)
        => new BitMask16 { value = val };

    public static implicit operator ushort(BitMask16 bm)
        => bm.value;

    public override string ToString()
        => Convert.ToString(value, 2);

    public bool Equals(BitMask16 other)
        => value == other.value;

    public void Invert()
        => value = (ushort)~value;

    public void ShiftLeft(int count)
        => value = (ushort)(value << count);

    public void ShiftRight(int count)
        => value = (ushort)(value >> count);

    public BitMask16 AND(BitMask16 other)
        => (ushort)(value & other);

    public BitMask16 OR(BitMask16 other)
        => (ushort)(value | other);

    public BitMask16 XOR(BitMask16 other)
        => (ushort)(value ^ other);

    public BitMask32 AND(BitMask32 other)
        => value & other;

    public BitMask32 OR(BitMask32 other)
        => value | other;

    public BitMask32 XOR(BitMask32 other)
        => value ^ other;

    public bool this[int i]
    {
        get => (this & 1 << i) > 0;
        set => this = value ? (ushort)(this | (1 << i))
            : (ushort)(this & ~(1 << i));
    }
}

/// <summary>
/// Represents a mask with 32 bits.
/// Single bits can be accessed like an array.
/// Uses a uint as backing value
/// and can implicitly converted from and to uint.
/// </summary>
public struct BitMask32 : IEquatable<BitMask32>, IBitMask
{
    private uint value;

    public static implicit operator BitMask32(uint val)
        => new BitMask32 { value = val };

    public static implicit operator uint(BitMask32 bm)
        => bm.value;

    public static BitMask32 operator ~(BitMask32 bm)
    => ~bm.value;

    public static BitMask32 operator ^(BitMask32 a, BitMask32 b)
        => (a.value ^ b.value);

    public override string ToString()
        => Convert.ToString(value, 2);

    public bool Equals(BitMask32 other)
    => value == other.value;

    public void Invert()
        => value = ~value;

    public void ShiftLeft(int count)
        => value <<= count;

    public void ShiftRight(int count)
        => value >>= count;

    public BitMask16 AND(BitMask16 other)
        => (ushort)(value & other);

    public BitMask16 OR(BitMask16 other)
        => (ushort)(value | other);

    public BitMask16 XOR(BitMask16 other)
        => (ushort)(value | other);

    public BitMask32 AND(BitMask32 other)
        => value & other;

    public BitMask32 OR(BitMask32 other)
        => value | other;

    public BitMask32 XOR(BitMask32 other)
        => value ^ other;

    public bool this[int i]
    {
        get => (this & 1 << i) > 0;
        set => this = value ? this | (uint)(1 << i)
            : this & ~(uint)(1 << i);
    }
}
