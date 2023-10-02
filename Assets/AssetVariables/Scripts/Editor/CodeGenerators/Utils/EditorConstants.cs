namespace LovelyBytes.AssetVariables
{
    internal static class EditorConstants
    {
        internal const string AssetMenuBasePath = "Window/AssetVariables/";
        
        internal const string TargetDirectoryRuntime = "Assets/Plugins/AssetVariables/Runtime/";
        internal const string TargetDirectoryEditor = "Assets/Plugins/AssetVariables/Editor/";
        
        internal const string TypeNameKeyword = "%TYPENAME%";
        internal const string FieldKeyword = "%FIELDS%";
        internal const string EditorFieldKeyword = "%EDITOR_FIELDS%";
        
        internal const string VariableDestPath = TargetDirectoryRuntime + TypeNameKeyword + "Variable.cs";
        internal const string ListenerDestPath = TargetDirectoryRuntime + TypeNameKeyword + "Listener.cs";
        internal const string EditorDestPath = TargetDirectoryEditor + TypeNameKeyword + "Editor.cs";

        internal const string RuntimeAsmRef = "{\"reference\": \"LovelyBytes.AssetVariables.Runtime\" }";
        internal const string EditorAsmRef = "{\"reference\": \"LovelyBytes.AssetVariables.Editor\" }";
    }
}