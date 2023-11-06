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
    }
}
