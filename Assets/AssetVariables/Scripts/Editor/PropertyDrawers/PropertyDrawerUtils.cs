using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    internal static class PropertyDrawerUtils 
    {
        internal static object GetParentObject(string path, object obj)
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
        
        internal static void NotifySetter(SerializedProperty property, GetSetAttribute getSetAttribute, FieldInfo fieldInfo)
        {
            object parent = PropertyDrawerUtils.GetParentObject(
                property.propertyPath, property.serializedObject.targetObject);

            if (parent == null)
            {
                getSetAttribute.IsDirty = false;
                return;
            }

            System.Type type = parent.GetType();
            PropertyInfo propertyInfo = type.GetProperty(getSetAttribute.Name);

            if (propertyInfo == null)
                Debug.LogError($"Invalid property name \"{getSetAttribute.Name}\" for GetSetAttribute");
            else
                propertyInfo.SetValue(parent, fieldInfo.GetValue(parent), null);

            getSetAttribute.IsDirty = false;
        }
    }
}
