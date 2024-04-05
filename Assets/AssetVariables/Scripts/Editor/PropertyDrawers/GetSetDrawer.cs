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
            return childCount * base.GetPropertyHeight(property, label) * 1.1f;
        }
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (attribute is not GetSetAttribute getSetAttribute)
                return;
            
            EditorGUI.BeginChangeCheck();

            EditorGUI.PropertyField(position, property, label, includeChildren: true);

            if (!EditorGUI.EndChangeCheck()) 
                return;

            if (Application.isPlaying)
                PropertyDrawerUtils.NotifySetter(property, getSetAttribute, fieldInfo);
            else
                property.serializedObject.ApplyModifiedProperties();
        }
    }
}
