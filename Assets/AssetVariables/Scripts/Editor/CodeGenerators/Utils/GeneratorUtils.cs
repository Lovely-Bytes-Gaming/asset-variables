using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    internal static class GeneratorUtils
    {
        // needed to extract namespace string from assembly
        private struct NamespaceProperty
        {
            public string rootNamespace;
        }
        
        internal static bool HasDuplicateElements(Entry[] entries)
        {
            int uniqueElementCount = entries
                .GroupBy(entry => entry.Name.ToLower())
                .Count();
            
            return uniqueElementCount != entries.Length;
        }
        
        internal static bool IsNameValid(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;
            
            // should start with a letter and can only contain numbers, letters and underscores
            var validName = new Regex("^[a-zA-Z]+[0-9a-zA-Z_]*$");
            return validName.IsMatch(name);
        }

        internal static string GetParentDirectory(string className)
        {
            string filePath = GetFilePath(className);
            int index = filePath.LastIndexOf('/');
            return filePath[..index];
        }

        internal static string ValueOrEmpty(in string value)
        {
            return string.IsNullOrEmpty(value)
                ? "Empty String"
                : value;
        }

        internal static string GetSelectionDirectory()
        {
            if (!Selection.activeObject)
                return "Assets";
            
            string path = AssetDatabase.GetAssetPath (Selection.activeObject);
            
            if (path == "")
            {
                path = "Assets";
            }
            else if (Path.GetExtension(path) != "")
            {
                path = path.Replace(Path.GetFileName (AssetDatabase.GetAssetPath (Selection.activeObject)), "");
            }
            return path;
        }

        internal static void SelectDirectoryGUI(ref string currentDirectory)
        {
            string label = string.IsNullOrEmpty(currentDirectory) ? "EMPTY" : currentDirectory;
            
            EditorGUILayout.LabelField("Destination Directory", label);

            if (!GUILayout.Button("Change Folder")) 
                return;
            
            string selection = EditorUtility.OpenFolderPanel("Select Folder", "", "");

            if(!string.IsNullOrEmpty(selection))
                currentDirectory = selection;
        }

        internal static string GetRootNamespace(string filePath)
        {
            // avoid endless loop
            for(int i = 0; i < 10000; ++i)
            {
                if (string.IsNullOrEmpty(filePath))
                    break;
                
                string[] files = Directory.GetFiles(filePath);

                foreach (string file in files)
                {
                    if (file.EndsWith(".asmdef"))
                    {
                        string content = File.ReadAllText(file);
                        return JsonUtility.FromJson<NamespaceProperty>(content).rootNamespace;
                    }                    
                }

                int removeIndex = filePath.LastIndexOf('/');
                
                if (removeIndex < 0)
                    break;
                
                filePath = filePath.Remove(removeIndex);
            }

            return null;
        }
        
        private static string GetFilePath(string className)
        {
            string[] guids = AssetDatabase.FindAssets($"t:Script {className}");
            
            return guids is { Length: > 0 } 
                ? AssetDatabase.GUIDToAssetPath(guids[0]) 
                : null;
        }
        
        
    }
}
