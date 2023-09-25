using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;

namespace LovelyBytesGaming.AssetVariables
{
    internal static class Utils
    {
        internal static bool HasDuplicateElements(Entry[] entries)
        {
            int uniqueElementCount = entries
                .GroupBy(entry => entry.Name.ToLower())
                .Count();
            
            return uniqueElementCount != entries.Length;
        }
        
        internal static string TypeToEditorField(PrimitiveType primitiveType, string label)
        {
            string fieldTemplate = (primitiveType != PrimitiveType._Quaternion)
                ? "value.{0} = EditorGUILayout.{1}(\"{0}\", value.{0});"
                : "value.{0} = Quaternion.Euler(EditorGUILayout.{1}(\"{0}\", value.{0}.eulerAngles));";

            string typeStr;

            switch (primitiveType)
            {
                case PrimitiveType._bool:
                    typeStr = "Toggle";
                    break;
                case PrimitiveType._Quaternion:
                    typeStr = "Vector3Field";
                    break;
                default:
                    typeStr = char.ToUpper(primitiveType.ToString()[1]) 
                              + primitiveType.ToString()[2..] + "Field";
                    break;
            }
            return string.Format(fieldTemplate, label, typeStr);
        }
        
        internal static bool IsVariableNameValid(string name)
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
