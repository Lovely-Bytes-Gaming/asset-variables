using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace LovelyBytesGaming.AssetVariables
{
    internal class StructVariableGenerator : BaseGenerator<StructVariableGenerator>
    {
        private static string PathToVariableTemplate 
            => ParentDirectory + "/Templates/StructVariable.txt";
        private static string PathToListenerTemplate 
            => ParentDirectory + "/Templates/StructListener.txt";
        private static string PathToEditorTemplate 
            => ParentDirectory + "/Templates/StructEditor.txt";
        
        private static string _typeName = "";
        private static Entry[] _entries;

        [MenuItem(Constants.AssetMenuBasePath + "Create New Struct Type")]
        public static void CreateNewType()
        {
            ShowWindow();
        }

        protected override bool HasUserInput()
        {
            GUILayout.Label("Custom Struct Generator", EditorStyles.boldLabel);
            GUILayout.Space(40f);
            _typeName = EditorGUILayout.TextField("Type Name: ", _typeName);
            GUILayout.Space(10f);
            
            GUILayout.Label("Custom Value Type Generator", EditorStyles.boldLabel);
            GUILayout.Space(40f);
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
                string fieldName = string.IsNullOrEmpty(_entries[i].Name)
                    ? $"Field{i}"
                    : _entries[i].Name;

                _entries[i].IsExpanded = EditorGUILayout.Foldout(_entries[i].IsExpanded, fieldName);
                
                if (!_entries[i].IsExpanded) 
                    continue;
                
                _entries[i].Name = EditorGUILayout.TextField("Name: ", _entries[i].Name ?? fieldName);
                _entries[i].PrimitiveType = (PrimitiveType)EditorGUILayout.EnumPopup("Type: ", _entries[i].PrimitiveType);
            }

            GUILayout.Space(20f);
            return true;
        }

        protected override bool IsInputValid()
        {
            if (!Utils.IsNameValid(_typeName))
            {
                EditorUtility.DisplayDialog(
                    $"Invalid Class Name: {Utils.ValueOrEmpty(_typeName)}",
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
                if (Utils.IsNameValid(e.Name)) 
                    continue;
                    
                EditorUtility.DisplayDialog(
                    $"Invalid Field Name: {Utils.ValueOrEmpty(e.Name)}",
                    "should start with a letter and should only contain letters, numbers and underscores.",
                    "Alrighty then");
                return false;
            }

            if (Utils.HasDuplicateElements(_entries))
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
                fieldDeclarations += $"public {e.PrimitiveType.ToString()[1..]} {e.Name};\n\t\t");

            string editorFields = "";

            for (int i = 0; i < _entries.Length; ++i)
            {
                editorFields += Utils.TypeToEditorField(_entries[i].PrimitiveType, _entries[i].Name);
                
                if (i + 1 < _entries.Length)
                    editorFields += "\n\t\t";
            }

            return new KeyValuePair<string, string>[]
            {
                new (Constants.TypeNameKeyword, _typeName),
                new (Constants.FieldKeyword, fieldDeclarations),
                new (Constants.EditorFieldKeyword, editorFields),
            };
        }

        protected override FileMapping[] GetFileMappings()
        {
            return new FileMapping[]
            {
                new()
                {
                    SourcePath = PathToVariableTemplate,
                    DestinationPath = Constants.VariableDestPath.Replace(Constants.TypeNameKeyword, _typeName)
                },
                new()
                {
                    SourcePath = PathToEditorTemplate,
                    DestinationPath = Constants.EditorDestPath.Replace(Constants.TypeNameKeyword, _typeName)
                },
                new()
                {
                    SourcePath = PathToListenerTemplate,
                    DestinationPath = Constants.ListenerDestPath.Replace(Constants.TypeNameKeyword, _typeName)
                }
            };
        }
    }
}