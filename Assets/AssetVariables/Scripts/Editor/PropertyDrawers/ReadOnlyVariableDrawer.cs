
using UnityEditor;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    [CustomPropertyDrawer(typeof(ReadOnlyVariable<>), true)]
    public class ReadOnlyVariableDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty variable = property.FindPropertyRelative("_value");

            if (!variable.objectReferenceValue)
                return base.GetPropertyHeight(property, label);
            
            SerializedProperty valueProp = new SerializedObject(variable.objectReferenceValue)
                .FindProperty("_value");

            if (!valueProp.isExpanded) 
                return base.GetPropertyHeight(valueProp, label);
            
            return base.GetPropertyHeight(valueProp, label) * valueProp.CountInProperty() * 1.1f;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label.text += " (Read Only)";
            SerializedProperty variable = property.FindPropertyRelative("_value");
            EditorGUI.PropertyField(position, variable, label);
        }
    }
}