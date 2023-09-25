using System;
using UnityEngine;
using UnityEditor;

namespace LovelyBytesGaming.AssetVariables
{
    public class CustomVariableGenerator : EditorWindow
    {
        private static string ParentDirectory
            => Utils.GetParentDirectory(nameof(CustomVariableGenerator));
        private static string PathToVariableTemplate 
            => ParentDirectory + "/Templates/CustomVariable.txt";
        private static string PathToListenerTemplate 
            => ParentDirectory + "/Templates/CustomVariableListener.txt";
        private static string PathToEditorTemplate 
            => ParentDirectory + "/Templates/CustomVariableEditor.txt";
        
        private static string _typeName = "";
        private static Entry[] _entries;

        [MenuItem("Window/Scriptable Variables/Create New Type")]
        public static void CreateNewType()
        {
            var window = (CustomVariableGenerator)GetWindow(typeof(CustomVariableGenerator));
            window.Show();
        }

        private void OnGUI()
        {
            if (!IsUserInputProvided()) 
                return;
            
            if (!GUILayout.Button("Generate"))
                return;

            if (!IsInputValid())
                return;

            GenerateSourceFiles();
        }

        private static bool IsUserInputProvided()
        {
            GUILayout.Label("Custom Value Type Generator", EditorStyles.boldLabel);
            GUILayout.Space(40f);
            _typeName = EditorGUILayout.TextField("Type Name: ", _typeName);
            GUILayout.Space(10f);

            int currentFieldCount = _entries?.Length ?? 0;
            int desiredFieldCount = EditorGUILayout.IntField("Number of Fields: ", currentFieldCount);

            if (desiredFieldCount <= 0)
                return false;
            
            if (_entries == null)
                _entries = new Entry[desiredFieldCount];
            else if (desiredFieldCount != currentFieldCount)
                Array.Resize(ref _entries, desiredFieldCount);

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

        private static bool IsInputValid()
        {
            if (!Utils.IsVariableNameValid(_typeName))
            {
                EditorUtility.DisplayDialog(
                    $"Invalid Class Name: {_typeName}",
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
                if (Utils.IsVariableNameValid(e.Name)) 
                    continue;
                    
                EditorUtility.DisplayDialog(
                    $"Invalid Field Name: {e.Name}",
                    "should start with a letter and should only contain letters, numbers and underscores.",
                    "Alrighty then");
                return false;
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
                            return false;
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
                return false;
            }
            return true;
        }

        private static void GenerateSourceFiles()
        {
            var fieldDeclarations = "";

            System.Array.ForEach(_entries,
                (Entry e) =>
                    fieldDeclarations += $"public {e.PrimitiveType.ToString()[1..]} {e.Name};\n\t\t");

            string editorFields = "";

            for (int i = 0; i < _entries.Length; ++i)
            {
                editorFields += Utils.TypeToEditorField(_entries[i].PrimitiveType, _entries[i].Name);
                
                if (i + 1 < _entries.Length)
                    editorFields += "\n\t\t";
            }
                
            try
            {
                FileWriter fileWriter = new();
                    
                InitializePluginFolder(fileWriter);
                WriteAllFiles(fileWriter, fieldDeclarations, editorFields);
                DisplaySuccessDialog();
                    
                AssetDatabase.Refresh();
            }
            catch (FileWriter.Exception e)
            {
                DisplayFailureDialog(e.Message);
            }
        }
        
        private static void InitializePluginFolder(FileWriter fileWriter)
        {
            if (FileWriter.DirectoryExists(Constants.TargetDirectoryRuntime)) 
                return;
            
            fileWriter.SetContent(Constants.RuntimeAsmRef);
            fileWriter.WriteFile(Constants.TargetDirectoryRuntime + "Runtime.asmref");
                        
            fileWriter.SetContent(Constants.EditorAsmRef);
            fileWriter.WriteFile(Constants.TargetDirectoryEditor + "Editor.asmref");
        }

        private static void WriteAllFiles(
            FileWriter fileWriter,
            string fieldDeclarations,
            string editorFields)
        {
            string variableFile = Constants.ClassDestPath.Replace(Constants.TypeNameKeyword, _typeName);
            string editorFile = Constants.EditorDestPath.Replace(Constants.TypeNameKeyword, _typeName);
            string listenerFile = Constants.ListenerDestPath.Replace(Constants.TypeNameKeyword, _typeName);
            
            fileWriter.LoadFile(PathToVariableTemplate);
            fileWriter.SetKeyword(Constants.TypeNameKeyword, _typeName);
            fileWriter.SetKeyword(Constants.FieldKeyword, fieldDeclarations);
            fileWriter.WriteFile(variableFile);
                    
            fileWriter.LoadFile(PathToEditorTemplate);
            fileWriter.SetKeyword(Constants.TypeNameKeyword, _typeName);
            fileWriter.SetKeyword(Constants.EditorFieldKeyword, editorFields);
            fileWriter.WriteFile(editorFile);
                    
            fileWriter.LoadFile(PathToListenerTemplate);
            fileWriter.SetKeyword(Constants.TypeNameKeyword, _typeName);
            fileWriter.SetKeyword(Constants.FieldKeyword, fieldDeclarations);
            fileWriter.WriteFile(listenerFile);
        }

        private static void DisplaySuccessDialog()
        {
            EditorUtility.DisplayDialog(
                "Success",
                $"created class script\n\n{Constants.ClassDestPath.Replace(Constants.TypeNameKeyword, _typeName)}\n\n" +
                $"listener script\n\n{Constants.ListenerDestPath.Replace(Constants.TypeNameKeyword, _typeName)}\n\n" +
                $"and editor script\n\n{Constants.EditorDestPath.Replace(Constants.TypeNameKeyword, _typeName)}",
                "Nicenstein"
            );
        }

        private static void DisplayFailureDialog(in string failureMessage)
        {
            EditorUtility.DisplayDialog("Failed to write source Files", failureMessage, "Sad");
        }
    }
}