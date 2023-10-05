using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;

namespace LovelyBytes.AssetVariables
{
    internal static class GeneratorUtils
    {
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
        
        private static string GetFilePath(string className)
        {
            string[] guids = AssetDatabase.FindAssets($"t:Script {className}");
            
            return guids is { Length: > 0 } 
                ? AssetDatabase.GUIDToAssetPath(guids[0]) 
                : null;
        }
    }
}
