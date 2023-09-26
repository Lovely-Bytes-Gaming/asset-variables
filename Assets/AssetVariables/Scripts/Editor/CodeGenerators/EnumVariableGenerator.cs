using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace LovelyBytesGaming.AssetVariables
{
    internal class EnumVariableGenerator : BaseGenerator<EnumVariableGenerator>
    {
        private static string PathToVariableTemplate
            => ParentDirectory + "/Templates/EnumVariable.txt";

        private static string PathToListenerTemplate
            => ParentDirectory + "/Templates/EnumListener.txt";

        private static string PathToEditorTemplate
            => ParentDirectory + "/Templates/EnumEditor.txt";

        private static string _typeName = "";
        private static string[] _values;
        private static int _defaultValueIndex = 0;

        [MenuItem(Constants.AssetMenuBasePath + "Create New Enum Type")]
        public static void CreateNewEnumType()
        {
            ShowWindow();
        }

        protected override bool HasUserInput()
        {
            GUILayout.Label("Enum Type Generator", EditorStyles.boldLabel);
            GUILayout.Space(40f);
            _typeName = EditorGUILayout.TextField("Enum Name: ", _typeName);
            GUILayout.Space(10f);

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

            GUILayout.Space(10f);
            _defaultValueIndex = EditorGUILayout.IntField("Default Value: ", _defaultValueIndex);
            if (_values != null)
            {
                _defaultValueIndex = (_defaultValueIndex < _values.Length) ? _defaultValueIndex : 0;
                GUILayout.Label($"({_values[_defaultValueIndex]})");
            }

            return _values != null && _values.Length != 0;
        }

        protected override bool IsInputValid()
        {
            if (!Utils.IsNameValid(_typeName))
            {
                EditorUtility.DisplayDialog(
                    "Invalid Class Name",
                    "Enum Variables should start with a letter and should only contain letters, numbers and underscores.",
                    "Alrighty then");
                return false;
            }

            string fieldString = "";

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
                if (Utils.IsNameValid(_values[i])) 
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
            string valueString = "";
            
            for (int i = 0; i < _values.Length; ++i)
            {
                if (!_values[i].StartsWith('_'))
                    valueString += '_';

                valueString += _values[i];

                if (i + 1 < _values.Length)
                    valueString += ",\n\t\t\t";
            }

            return new KeyValuePair<string, string>[]
            {
                new(Constants.TypeNameKeyword, _typeName),
                new(Constants.FieldKeyword, valueString),
                new(Constants.DefaultValueKeyword, _values[_defaultValueIndex]),
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

