using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace LovelyBytes.AssetVariables
{
    internal class EnumVariableGenerator : BaseGenerator<EnumVariableGenerator>
    {
        private static string PathToEnumTemplate 
            => ParentDirectory + "/Templates/Enum.txt";

        private static string _typeName = "";
        private static string[] _values;

        [MenuItem(EditorConstants.AssetMenuBasePath + "Create New Enum Type")]
        public static void CreateNewEnumType()
        {
            ShowWindow();
        }

        protected override bool HasUserInput()
        {
            base.HasUserInput();
            GUILayout.Space(40f);
            GUILayout.Label("Enum Type Generator", EditorStyles.boldLabel);
            GUILayout.Space(20f);
            _typeName = EditorGUILayout.TextField("Enum Name: ", _typeName);
            GUILayout.Space(20f);

            int currentValueCount = _values?.Length ?? 0;
            int desiredValueCount = EditorGUILayout.IntField("Number of Values: ", currentValueCount);

            if (desiredValueCount <= 0)
                return false;

            if (_values == null)
            {
                _values = new string[desiredValueCount];
            }
            else if (desiredValueCount != currentValueCount)
            {
                Array.Resize(ref _values, desiredValueCount);
            }

            for (int i = 0; i < desiredValueCount; ++i)
            {
                _values[i] = EditorGUILayout.TextField($"{i}", _values[i] ?? $"Value{i}");
            }

            return _values != null && _values.Length != 0;
        }

        protected override bool IsInputValid()
        {
            if (!base.IsInputValid())
                return false;
            
            if (!GeneratorUtils.IsNameValid(_typeName))
            {
                EditorUtility.DisplayDialog(
                    "Invalid Class Name",
                    "Enum Variables should start with a letter and should only contain letters, numbers and underscores.",
                    "Alrighty then");
                return false;
            }

            if (_values.Length < 2)
            {
                if (!EditorUtility.DisplayDialog(
                        "Pointless Enum?",
                        "You are trying to create an enum with just one value...\nU sure?",
                        "Just do it >:(",
                        "my mistake"))
                {
                    return false;
                }
            }

            if (_values.Distinct().Count() != _values.Length)
            {
                EditorUtility.DisplayDialog("Error", "Your enum contains duplicate value names.", "Sad");
                return false;
            }
            
            for (int i = 0; i < _values.Length; ++i)
            {
                if (GeneratorUtils.IsNameValid(_values[i])) 
                    continue;
                
                EditorUtility.DisplayDialog(
                    $"Invalid Name: {_values[i]}",
                    "Enum Values should start with a letter and should only contain letters, numbers and underscores.",
                    "Alrighty then");
                
                return false;
            }

            return true;
        }

        protected override KeyValuePair<string, string>[] GetKeywordValues()
        {
            string valueString = string.Empty;
            
            for (int i = 0; i < _values.Length; ++i)
            {
                valueString += _values[i];

                if (i + 1 < _values.Length)
                    valueString += ",\n\t";
            }

            return new KeyValuePair<string, string>[]
            {
                new(EditorConstants.TypeNameKeyword, _typeName),
                new(EditorConstants.FieldKeyword, valueString),
                new(EditorConstants.PackageNameSpaceKeyword, EditorConstants.PackageNameSpace)
            };
        }

        protected override FileMapping[] GetFileMappings()
        {
            return new FileMapping[]
            {
                new()
                {
                    SourcePath  = PathToEnumTemplate,
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

