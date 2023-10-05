using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    [CustomPropertyDrawer(typeof(GetSetAttribute))]
    public class GetSetDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!property.isExpanded)
                return base.GetPropertyHeight(property, label);

            int childCount = property.CountInProperty();
            return childCount * 20f;
        }
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (attribute is not GetSetAttribute getSetAttribute)
                return;
            
            EditorGUI.BeginChangeCheck();

            EditorGUI.PropertyField(position, property, label, includeChildren: true);
            
            if (EditorGUI.EndChangeCheck())
            {
                getSetAttribute.IsDirty = Application.isPlaying;
            }
            else if (getSetAttribute.IsDirty)
            {
                object parent = GetParentObject(property.propertyPath, property.serializedObject.targetObject);

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

        private static object GetParentObject(string path, object obj)
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
    }
}
