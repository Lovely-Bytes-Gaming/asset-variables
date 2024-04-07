
using UnityEditor;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    [CustomPropertyDrawer(typeof(DefaultValue<>), useForChildren: true)]
    public class DefaultValueDrawer : PropertyDrawer
    {
        private Texture2D _removeIcon;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty valueProperty = property.FindPropertyRelative("Value");
            
            if (!valueProperty.isExpanded)
                return base.GetPropertyHeight(property, label);

            return valueProperty.CountInProperty() * base.GetPropertyHeight(property, label) * 1.1f;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty useProperty = property.FindPropertyRelative("Use");
            if (!useProperty.boolValue)
            {
                EditorGUI.BeginChangeCheck();
                EditorGUI.PropertyField(position, useProperty, new GUIContent("Add default value"));
                
                if (EditorGUI.EndChangeCheck())
                    property.serializedObject.ApplyModifiedProperties();

                return;
            }

            const float buttonWidth = 20f;
            const float padding = 2f;

            if(!_removeIcon)
                PropertyDrawerUtils.LoadIcon("remove.png", out _removeIcon);
            
            SerializedProperty valueProperty = property.FindPropertyRelative("Value");

            Rect fieldPos = position;
            fieldPos.width -= buttonWidth + padding;

            Rect buttonPos = position;
            buttonPos.width = buttonWidth;
            buttonPos.x = fieldPos.x + fieldPos.width + padding;

            GUIContent buttonContent = new(string.Empty, "Don't use default value");

            GUIStyle buttonStyle = new(EditorStyles.miniButtonRight)
            {
                normal =
                {
                    background = _removeIcon,
                }
            };
            
            EditorGUI.BeginChangeCheck();
            EditorGUI.PropertyField(fieldPos, valueProperty, label, true);
            bool remove = GUI.Button(buttonPos, buttonContent, buttonStyle);
            
            if (remove)
            {
                useProperty.boolValue = false;
            }

            if (remove || EditorGUI.EndChangeCheck())
                property.serializedObject.ApplyModifiedProperties();
        }
    }
}