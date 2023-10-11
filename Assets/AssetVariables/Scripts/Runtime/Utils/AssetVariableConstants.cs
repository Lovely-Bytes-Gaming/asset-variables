using System.Threading;

namespace LovelyBytes.AssetVariables
{
    public static class AssetVariableConstants
    {
        public const string DefaultAssetPath = "LovelyBytes/AssetVariables/";
        internal const int MaxSetValueRecursionDepth = 100;
    }

    internal static class MainThread
    {
        // Explicit static constructor to prevent compiler from marking this type as 'beforefieldinit'
        // This ensures that all field initializers will only be called on first access 
        static MainThread() 
        {  }
        
        internal static readonly int ID = Thread.CurrentThread.ManagedThreadId;
    }
}
