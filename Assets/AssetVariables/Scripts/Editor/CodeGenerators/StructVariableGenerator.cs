using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace LovelyBytes.AssetVariables
{
    internal class StructVariableGenerator : BaseGenerator<StructVariableGenerator>
    {
        private static string PathToStructTemplate 
            => ParentDirectory + "/Templates/Struct.txt";
        
        private static string _typeName = "";
        private static Entry[] _entries;

        [MenuItem(EditorConstants.AssetMenuBasePath + "Create New Struct Type")]
        public static void CreateNewType()
        {
            ShowWindow();
        }

        protected override bool HasUserInput()
        {
            base.HasUserInput();
            GUILayout.Space(20f);
            GUILayout.Label("Custom Struct Generator", EditorStyles.boldLabel);
            GUILayout.Space(10f);
            _typeName = EditorGUILayout.TextField("Type Name: ", _typeName);
            GUILayout.Space(10f);

            int currentFieldCount = _entries?.Length ?? 0;
            int desiredFieldCount = EditorGUILayout.IntField("Number of Fields: ", currentFieldCount);

            if (desiredFieldCount <= 0)
                return false;

            if (_entries == null)
            {
                _entries = new Entry[desiredFieldCount];
            }
            else if (desiredFieldCount != currentFieldCount)
            {
                Array.Resize(ref _entries, desiredFieldCount);
            }

            for (int i = 0; i < desiredFieldCount; ++i)
            {
                if (string.IsNullOrEmpty(_entries[i].Name))
                    _entries[i].Name = $"Field{i}";

                _entries[i].IsExpanded = EditorGUILayout.Foldout(_entries[i].IsExpanded, _entries[i].Name);
                
                if (!_entries[i].IsExpanded) 
                    continue;
                
                _entries[i].Name = EditorGUILayout.TextField("Name: ", _entries[i].Name);
                _entries[i].PrimitiveType = (PrimitiveType)EditorGUILayout.EnumPopup("Type: ", _entries[i].PrimitiveType);
            }
            
            GUILayout.Space(20f);
            return true;
        }

        protected override bool IsInputValid()
        {
            if (!base.IsInputValid())
                return false;
            
            if (!GeneratorUtils.IsNameValid(_typeName))
            {
                EditorUtility.DisplayDialog(
                    $"Invalid Class Name: {GeneratorUtils.ValueOrEmpty(_typeName)}",
                    "should start with a letter and should only contain letters, numbers and underscores.",
                    "Alrighty then");
                return false;
            }

            if (_entries == null)
            {
                EditorUtility.DisplayDialog(
                    "Not enough fields",
                    "Custom structs should have at least one field.",
                    "Alrighty then");
                return false;
            }

            foreach (Entry e in _entries)
            {
                if (GeneratorUtils.IsNameValid(e.Name)) 
                    continue;
                    
                EditorUtility.DisplayDialog(
                    $"Invalid Field Name: {GeneratorUtils.ValueOrEmpty(e.Name)}",
                    "should start with a letter and should only contain letters, numbers and underscores.",
                    "Alrighty then");
                return false;
            }

            if (GeneratorUtils.HasDuplicateElements(_entries))
            {
                EditorUtility.DisplayDialog("Error", "Your Type contains duplicate field names (ignoring case).", "Sad");
                return false;
            }
            return true;
        }

        protected override KeyValuePair<string, string>[] GetKeywordValues()
        {
            var fieldDeclarations = "";

            System.Array.ForEach(_entries, e =>
                fieldDeclarations += $"public {e.PrimitiveType.ToString()[1..]} {e.Name};\n\t");

            return new KeyValuePair<string, string>[]
            {
                new (EditorConstants.TypeNameKeyword, _typeName),
                new (EditorConstants.FieldKeyword, fieldDeclarations),
                new (EditorConstants.PackageNameSpaceKeyword, EditorConstants.PackageNameSpace)
            };
        }

        protected override FileMapping[] GetFileMappings()
        {
            return new FileMapping[]
            {
                new()
                {
                    SourcePath = PathToStructTemplate,
                    FileName = $"{_typeName}.cs"
                },
                new()
                {
                    SourcePath = PathToVariableTemplate,
                    FileName = $"{_typeName}Variable.cs"
                },
                new()
                {
                    SourcePath = PathToListenerTemplate,
                    FileName = $"{_typeName}Listener.cs"
                }
            };
        }
    }
}