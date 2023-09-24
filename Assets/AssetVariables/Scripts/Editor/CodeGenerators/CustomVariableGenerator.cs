using UnityEngine;
using UnityEditor;
using System.IO;

namespace LovelyBytesGaming.AssetVariables
{
    public class CustomVariableGenerator : EditorWindow
    {
        private const string _classSrcPath = Constants.SourceDirectory + "CustomVariable.txt";
        private const string _listenerSrcPath = Constants.SourceDirectory + "CustomVariableListener.txt";
        private const string _editorSrcPath = Constants.SourceDirectory + "CustomVariableEditor.txt";
        
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
                    int entriesLength = Mathf.Min(tmp, _numValues);
                    for (int i = 0; i < entriesLength; ++i)
                    {
                        tmpEntries[i] = _entries[i];
                    }
                    _entries = tmpEntries;
                }
                _numValues = tmp;
            }

            for (int i = 0; i < _numValues; ++i)
            {
                string fieldName = string.IsNullOrEmpty(_entries[i].Name)
                    ? $"Field {i}"
                    : _entries[i].Name;

                _entries[i].IsExpanded = EditorGUILayout.Foldout(_entries[i].IsExpanded, fieldName);
                
                if (!_entries[i].IsExpanded) 
                    continue;
                
                _entries[i].Name = EditorGUILayout.TextField("Name: ", _entries[i].Name ?? "");
                _entries[i].PrimitiveType = (PrimitiveType)EditorGUILayout.EnumPopup("Type: ", _entries[i].PrimitiveType);
            }

            GUILayout.Space(20f);
            if (GUILayout.Button("Generate Scripts"))
            {
                if (!Utils.IsVariableNameValid(_nameStr))
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
                    if (Utils.IsVariableNameValid(e.Name)) 
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

                if (Utils.HasDuplicateElements(_entries))
                {
                    EditorUtility.DisplayDialog("Error", "Your Type contains duplicate field names (ignoring case).", "Sad");
                    return;
                }

                var fieldDeclarations = "";

                System.Array.ForEach(_entries,
                    (Entry e) =>
                        fieldDeclarations += $"public {e.PrimitiveType.ToString()[1..]} {e.Name};\n\t\t");

                int k = 0;

                string editorFields = "";

                k = 0;
                for (; k + 1 < _entries.Length; ++k)
                {
                    editorFields += Utils.TypeToEditorField(_entries[k].PrimitiveType, _entries[k].Name) + "\n\t\t";
                }
                editorFields += Utils.TypeToEditorField(_entries[k].PrimitiveType, _entries[k].Name);

                try
                {
                    string scriptTemplate = File.ReadAllText(_classSrcPath);
                    string listenerTemplate = File.ReadAllText(_listenerSrcPath);
                    string editorTemplate = File.ReadAllText(_editorSrcPath);

                    string scriptStr = scriptTemplate
                        .Replace(Constants.TypeNameKeyword, _nameStr)
                        .Replace(Constants.FieldKeyword, fieldDeclarations);

                    string listenerStr = listenerTemplate
                        .Replace(Constants.TypeNameKeyword, _nameStr);

                    string editorStr = editorTemplate
                        .Replace(Constants.TypeNameKeyword, _nameStr)
                        .Replace(Constants.FieldKeyword, editorFields);

                    FileInfo file = new FileInfo(Constants.ClassDestPath.Replace(Constants.TypeNameKeyword, _nameStr));
                    file.Directory.Create(); // If the directory already exists, this method does nothing.
                    File.WriteAllText(file.FullName, scriptStr);

                    file = new FileInfo(Constants.ListenerDestPath.Replace(Constants.TypeNameKeyword, _nameStr));
                    file.Directory.Create();
                    File.WriteAllText(file.FullName, listenerStr);

                    file = new FileInfo(Constants.EditorDestPath.Replace(Constants.TypeNameKeyword, _nameStr));
                    file.Directory.Create();
                    File.WriteAllText(file.FullName, editorStr);

                    EditorUtility.DisplayDialog(
                        "Success",
                        $"created class script\n\n{Constants.ClassDestPath.Replace(Constants.TypeNameKeyword, _nameStr)}\n\n" +
                        $"listener script\n\n{Constants.ListenerDestPath.Replace(Constants.TypeNameKeyword, _nameStr)}\n\n" +
                        $"and editor script\n\n{Constants.EditorDestPath.Replace(Constants.TypeNameKeyword, _nameStr)}",
                        "Nicenstein"
                    );
                    AssetDatabase.Refresh();
                }
                catch (System.Exception e)
                {
                    EditorUtility.DisplayDialog("Error", e.Message, "Sad");
                }
            }
        }
    }
}