namespace LovelyBytesGaming.AssetVariables
{
    /// <summary>
    /// Represents a mask with 32 bits.
    /// Single bits can be accessed like an array.
    /// Uses an int as backing value
    /// and can be implicitly converted from and to int.
    /// </summary>
    public struct BitMask32 
    {
        private int _value;

        public static implicit operator BitMask32(int val)
            => new BitMask32 { _value = val };

        public static implicit operator int(BitMask32 bm)
            => bm._value;

        public BitMask32(int value)
            => this._value = value;

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
