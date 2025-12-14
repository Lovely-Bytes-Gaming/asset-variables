using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    internal static class PropertyDrawerUtils 
    {
        public static object GetParentObject(string path, object obj)
        {
            string[] fields = path.Split('.');

            for (int i = 0; i < fields.Length-1; ++i)
            {
                Debug.Log(fields[i]);
                
                FieldInfo fieldInfo = obj
                    ?.GetType()
                    .GetField(fields[i], BindingFlags.Public | 
                                         BindingFlags.NonPublic | 
                                         BindingFlags.Instance);

                if (fieldInfo == null)
                    return null;
                
                obj = fieldInfo.GetValue(obj);
            }

            return obj;
        }
        
        public static void LoadIcon(string fileName, out Texture2D target)
        {
            string currentFolder = GeneratorUtils.GetParentDirectory(nameof(PropertyDrawerUtils));
            string assetPath = $"{currentFolder}/Icons/{fileName}";

            target = AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);
        }
        
        public static bool TryGetValueProperty(SerializedProperty property, out SerializedProperty valueProperty)
        {
            valueProperty = null;
            
            if (!property.objectReferenceValue)
                return false;
            
            SerializedObject targetObject = new(property.objectReferenceValue);
            valueProperty = targetObject.FindProperty("_value");
            return valueProperty != null;
        }
    }
}
