namespace LovelyBytes.AssetVariables
{
    internal static class EditorConstants
    {
        internal const string AssetMenuBasePath = "Assets/Create/LovelyBytes/AssetVariables/";
        internal const string PackageNameSpace = "LovelyBytes.AssetVariables";
        
        internal const string TargetDirectoryRuntime = "Assets/Plugins/AssetVariables/Runtime/";
        internal const string TargetDirectoryEditor = "Assets/Plugins/AssetVariables/Editor/";
        
        internal const string TypeNameKeyword = "%TYPENAME%";
        internal const string FieldKeyword = "%FIELDS%";
        
        internal const string NamespaceBeginKeyword = "%NAMESPACE_BEGIN%";
        internal const string NamespaceEndKeyword = "%NAMESPACE_END%";
        internal const string PackageNameSpaceKeyword = "%ASSET_VARIABLE_NAMESPACE%";
        
        internal const string VariableDestPath = TargetDirectoryRuntime + TypeNameKeyword + "Variable.cs";
        internal const string ListenerDestPath = TargetDirectoryRuntime + TypeNameKeyword + "Listener.cs";

        internal const string RuntimeAsmRef = "{\"reference\": \"LovelyBytes.AssetVariables.Runtime\" }";
        internal const string EditorAsmRef = "{\"reference\": \"LovelyBytes.AssetVariables.Editor\" }";
    }
}