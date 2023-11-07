using System;

namespace LovelyBytes.AssetVariables
{
    internal static class HelperFunctions 
    {
        internal static int RepeatIndex(int index, int count)
        {
            index %= count;
            
            if (index < 0)
                index += count;

            return index;
        } 
        
        public static bool AreEqual<TType>(TType oldValue, TType newValue)
            where TType : IComparable<TType>
        {
            if (IsNull(oldValue))
                return IsNull(newValue);

            if (IsNull(newValue))
                return IsNull(oldValue);

            return oldValue.CompareTo(newValue) == 0;
        }
        
        private static bool IsNull<TType>(TType value)
        {
            return ReferenceEquals(value, null);
        }
    }
}
