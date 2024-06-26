using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace LovelyBytes.AssetVariables
{
    internal static class PropertyDrawerUtils 
    {
        public static object GetParentObject(string path, object obj)
        {
            string[] fields = path.Split('.');

            for (int i = 0; i < fields.Length-1; ++i)
            {
                FieldInfo fieldInfo = obj
                    ?.GetType()
                    .GetField(fields[i], BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                if (fieldInfo == null)
                    return null;
                
                obj = fieldInfo.GetValue(obj);
            }

            return obj;
        }
        
        public static void NotifySetter(SerializedProperty property, GetSetAttribute getSetAttribute, 
            FieldInfo fieldInfo)
        {
            object parent = GetParentObject(property.propertyPath, property.serializedObject.targetObject);

            if (parent == null)
                return;
            
            object oldValue = fieldInfo.GetValue(parent);
            property.serializedObject.ApplyModifiedProperties();
            
            object newValue = fieldInfo.GetValue(parent);

            System.Type type = parent.GetType();
            PropertyInfo propertyInfo = type.GetProperty(getSetAttribute.Name);

            if (propertyInfo == null)
            {
                Debug.LogError($"Invalid property name \"{getSetAttribute.Name}\" for GetSetAttribute");
                return;
            }
            // Workaround to achieve correct setter behaviour:
            // Set the field back to its old value, then call the setter with the new value  
            fieldInfo.SetValue(parent, oldValue);
            propertyInfo.SetValue(parent, newValue, null);
        }
        
        public static void LoadIcon(string fileName, out Texture2D target)
        {
            string currentFolder = GeneratorUtils.GetParentDirectory(nameof(PropertyDrawerUtils));
            string assetPath = $"{currentFolder}/Icons/{fileName}";

            target = AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);
        }
    }
}
