using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LovelyBytesGaming.AssetVariables
{
    internal static class Constants
    {
        internal const string SourceDirectory = "Assets/AssetVariables/Scripts/Editor/CodeGenerators/Templates/";
        internal const string TargetDirectoryRuntime = "Assets/Plugins/AssetVariables/Runtime/";
        internal const string TargetDirectoryEditor = "Assets/Plugins/AssetVariables/Editor/";
        
        internal const string TypeNameKeyword = "%TYPENAME%";
        internal const string FieldKeyword = "%FIELDS%";
        internal const string EditorFieldKeyword = "%EDITOR_FIELDS%";
        
        internal const string ClassDestPath = TargetDirectoryRuntime + TypeNameKeyword + "Variable.cs";
        internal const string ListenerDestPath = TargetDirectoryRuntime + TypeNameKeyword + "Listener.cs";
        internal const string EditorDestPath = TargetDirectoryEditor + TypeNameKeyword + "Editor.cs";

        internal const string RuntimeAsmRef = "{\"reference\": \"LovelyBytesGaming.AssetVariables.Runtime\" }";
        internal const string EditorAsmRef = "{\"reference\": \"LovelyBytesGaming.AssetVariables.Editor\" }";
    }
}