namespace LovelyBytes.AssetVariables
{
    public static class AssetVariableConstants
    {
        public const string DefaultAssetPath = "LovelyBytes/AssetVariables/";
        internal const int MaxSetValueRecursionDepth = 100;
        
        // Technically not a constant, but can only be changed from within this assembly
        internal static int MainThreadID;
    }
}
