using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace LovelyBytesGaming.AssetVariables
{
    public class CustomVariableGenerator : EditorWindow
    {
        private struct Entry
        {
            public Type Type;
            public string Name;
            public bool IsExpanded;
        }
        
        private const string _sourceDirectory = "Assets/AssetVariables/Scripts/Editor/CodeGenerators/Templates/";
        private const string _targetDirectoryRuntime = "Assets/Plugins/AssetVariables/Runtime/";
        private const string _targetDirectoryEditor = "Assets/Plugins/AssetVariables/Editor/";
        
        private const string _classSrcPath = _sourceDirectory + "CustomVariable.txt";
        private const string _listenerSrcPath = _sourceDirectory + "CustomVariableListener.txt";
        private const string _editorSrcPath = _sourceDirectory + "CustomVariableEditor.txt";
        
        private const string _typeNameKeyword = "%TYPENAME%";
        private const string _fieldKeyword = "%FIELDS%";
        
        private const string _classDestPath = _targetDirectoryRuntime + _typeNameKeyword + "Variable.cs";
        private const string _listenerDestPath = _targetDirectoryRuntime + _typeNameKeyword + "Listener.cs";
        private const string _editorDestPath = _targetDirectoryEditor + _typeNameKeyword + "Editor.cs";
        
        private enum Type
        {
            _bool, 
            _int, 
            _long, 
            _float, 
            _double, 
            _Quaternion, 
            _Vector2, 
            _Vector3, 
            _Vector4, 
            _Vector2Int,
            _Vector3Int, 
        }

        private static string _nameStr = "";
        private static int _numValues;

        private static Entry[] _entries;

        [MenuItem("Window/Scriptable Variables/Create or delete Custom Value Type")]
        public static void CreateNewEnumType()
        {
            var window = (CustomVariableGenerator)GetWindow(typeof(CustomVariableGenerator));
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.Label("Custom Value Type Generator", EditorStyles.boldLabel);
            GUILayout.Space(40f);
            _nameStr = EditorGUILayout.TextField("Type Name: ", _nameStr);
            GUILayout.Space(10f);
            int tmp = EditorGUILayout.IntField("Number of Values: ", _numValues);

            if (tmp != _numValues)
            {
                if (_entries == null)
                    _entries = new Entry[tmp];
                else
                {
                    var tmpEntries = new Entry[tmp];
                    for (int i = 0; i < ((tmp < _numValues) ? tmp : _numValues); ++i)
                    {
                        tmpEntries[i] = _entries[i];
                    }
                    _entries = tmpEntries;
                }
                _numValues = tmp;
            }

            for (int i = 0; i < _numValues; ++i)
            {
                string fieldName = !string.IsNullOrEmpty(_entries[i].Name)
                    ? _entries[i].Name
                    : $"Field {i}";

                _entries[i].IsExpanded = EditorGUILayout.Foldout(_entries[i].IsExpanded, fieldName);
                
                if (!_entries[i].IsExpanded) 
                    continue;
                
                _entries[i].Name = EditorGUILayout.TextField("Name: ", _entries[i].Name ?? "");
                _entries[i].Type = (Type)EditorGUILayout.EnumPopup("Type: ", _entries[i].Type);
            }

            GUILayout.Space(20f);
            if (GUILayout.Button("Generate Scripts"))
            {
                if (!IsNameValid(_nameStr))
                {
                    EditorUtility.DisplayDialog(
                        $"Invalid Class Name: {_nameStr}",
                        "should start with a letter and should only contain letters, numbers and underscores.",
                        "Alrighty then");
                    return;
                }

                if (_entries == null)
                {
                    EditorUtility.DisplayDialog(
                        "Not enough fields",
                        "Custom structs should have at least one field.",
                        "Alrighty then");
                    return;
                }

                foreach (Entry e in _entries)
                {
                    if (IsNameValid(e.Name)) 
                        continue;
                    
                    EditorUtility.DisplayDialog(
                        $"Invalid Field Name: {e.Name}",
                        "should start with a letter and should only contain letters, numbers and underscores.",
                        "Alrighty then");
                    return;
                }

                var fallthrough = false;

                for (int j = 0; j < _entries.Length; ++j)
                {
                    if (!string.IsNullOrEmpty(_entries[j].Name)) 
                        continue;
                    
                    if (!fallthrough)
                    {
                        int choice = EditorUtility.DisplayDialogComplex(
                            "Incomplete Type?",
                            $"Field {j} is not named.\nIt will receive a default name.",
                            "Go ahead",
                            "Apply to all",
                            "Abort!");

                        switch (choice)
                        {
                            case 2:
                                return;
                            case 1:
                                fallthrough = true;
                                break;
                        }
                    }
                    _entries[j].Name = $"field_{j}";
                }

                if (_entries.GroupBy(e => e.Name.ToLower()).Select(g => g.First()).Count() != _entries.Length)
                {
                    EditorUtility.DisplayDialog("Error", "Your Type contains duplicate field names (ignoring case).", "Sad");
                    return;
                }

                var fieldDeclarations = "";

                System.Array.ForEach(_entries,
                    (Entry e) =>
                        fieldDeclarations += $"public {e.Type.ToString()[1..]} {e.Name};\n\t\t");

                int k = 0;

                string editorFields = "";

                k = 0;
                for (; k + 1 < _entries.Length; ++k)
                {
                    editorFields += TypeToEditorField(_entries[k].Type, _entries[k].Name) + "\n\t\t";
                }
                editorFields += TypeToEditorField(_entries[k].Type, _entries[k].Name);

                try
                {
                    string scriptTemplate = File.ReadAllText(_classSrcPath);
                    string listenerTemplate = File.ReadAllText(_listenerSrcPath);
                    string editorTemplate = File.ReadAllText(_editorSrcPath);

                    string scriptStr = scriptTemplate
                        .Replace(_typeNameKeyword, _nameStr)
                        .Replace(_fieldKeyword, fieldDeclarations);

                    string listenerStr = listenerTemplate
                        .Replace(_typeNameKeyword, _nameStr);

                    string editorStr = editorTemplate
                        .Replace(_typeNameKeyword, _nameStr)
                        .Replace(_fieldKeyword, editorFields);

                    FileInfo file = new FileInfo(_classDestPath.Replace(_typeNameKeyword, _nameStr));
                    file.Directory.Create(); // If the directory already exists, this method does nothing.
                    File.WriteAllText(file.FullName, scriptStr);

                    file = new FileInfo(_listenerDestPath.Replace(_typeNameKeyword, _nameStr));
                    file.Directory.Create();
                    File.WriteAllText(file.FullName, listenerStr);

                    file = new FileInfo(_editorDestPath.Replace(_typeNameKeyword, _nameStr));
                    file.Directory.Create();
                    File.WriteAllText(file.FullName, editorStr);

                    EditorUtility.DisplayDialog(
                        "Success",
                        $"created class script\n\n{_classDestPath.Replace(_typeNameKeyword, _nameStr)}\n\n" +
                        $"listener script\n\n{_listenerDestPath.Replace(_typeNameKeyword, _nameStr)}\n\n" +
                        $"and editor script\n\n{_editorDestPath.Replace(_typeNameKeyword, _nameStr)}",
                        "Nicenstein"
                    );
                    AssetDatabase.Refresh();
                }
                catch (System.Exception e)
                {
                    EditorUtility.DisplayDialog("Error", e.Message, "Sad");
                }
            }

            else if (GUILayout.Button("Find and Delete"))
            {
                try
                {
                    FileInfo file = new FileInfo(_classDestPath.Replace(_typeNameKeyword, _nameStr));
                    file.Delete();

                    file = new FileInfo(_listenerDestPath.Replace(_typeNameKeyword, _nameStr));
                    file.Delete();

                    file = new FileInfo(_editorDestPath.Replace(_typeNameKeyword, _nameStr));
                    file.Delete();

                    EditorUtility.DisplayDialog(
                        "Success",
                        $"deleted class script\n\n{_classDestPath.Replace(_typeNameKeyword, _nameStr)}\n\n" +
                        $"listener script\n\n{_listenerDestPath.Replace(_typeNameKeyword, _nameStr)}\n\n" +
                        $"and editor script\n\n{_editorDestPath.Replace(_typeNameKeyword, _nameStr)}",
                        "They had it coming"
                    );

                    AssetDatabase.Refresh();
                }
                catch (System.Exception e)
                {
                    EditorUtility.DisplayDialog("Error", e.Message, "Sad");
                }
            }
        }

        private static bool IsNameValid(string name)
        {
            // should start with a letter and can only contain numbers, letters and underscores
            Regex validName = new Regex("^[a-zA-Z]+[0-9a-zA-Z_]*$");
            return validName.IsMatch(name);
        }

        private string TypeToEditorField(Type type, string name)
        {
            string fieldTemplate = (type != Type._Quaternion)
                ? "value.{0} = EditorGUILayout.{1}(\"{0}\", value.{0});"
                : "value.{0} = Quaternion.Euler(EditorGUILayout.{1}(\"{0}\", value.{0}.eulerAngles));";

            string typeStr;

            switch (type)
            {
                case Type._bool:
                    typeStr = "Toggle";
                    break;
                case Type._Quaternion:
                    typeStr = "Vector3Field";
                    break;
                default:
                    typeStr = type.ToString().ElementAt(1).ToString().ToUpper() + type.ToString()[2..] + "Field";
                    break;
            }
            return string.Format(fieldTemplate, name, typeStr);
        }
    }
}

