using System;

public struct BitMask16 : IEquatable<BitMask16>
{
    private ushort value;

    public static implicit operator BitMask16(ushort val)
        => new BitMask16 { value = val };

    public static implicit operator ushort(BitMask16 bm)
        => bm.value;

    public static BitMask16 operator ~(BitMask16 bm)
        => (ushort)~bm.value;

    public static BitMask16 operator ^(BitMask16 a, BitMask16 b)
    => (ushort)(a ^ b);

    public override string ToString()
        => Convert.ToString(value, 2);

    public bool Equals(BitMask16 other)
        => this.value == other.value;

    public bool this[int i]
    {
        get => (this & 1 << i) > 0;
        set => this = value ? (ushort)(this | (1 << i))
            : (ushort)(this & ~(1 << i));
    }
}


public struct BitMask32 : IEquatable<BitMask32>
{
    private uint value;

    public static implicit operator BitMask32(uint val)
        => new BitMask32 { value = val };

    public static implicit operator uint(BitMask32 bm)
        => bm.value;

    public static BitMask32 operator ~(BitMask32 bm)
    => ~bm.value;

    public override string ToString()
        => Convert.ToString(value, 2);

    public bool Equals(BitMask32 other)
    => this.value == other.value;

    public bool this[int i]
    {
        get => (this & 1 << i) > 0;
        set => this = value ? this | (uint)(1 << i)
            : this & ~(uint)(1 << i);
    }
}
