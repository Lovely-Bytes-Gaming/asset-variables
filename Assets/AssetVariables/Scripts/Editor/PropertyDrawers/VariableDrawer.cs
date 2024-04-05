
using UnityEditor;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    [CustomPropertyDrawer(typeof(Variable<>), useForChildren: true)]
    public class VariableDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedObject targetObject = new(property.objectReferenceValue);
            SerializedProperty valueProp = targetObject.FindProperty("_value");

            if (!valueProp.isExpanded)
                return base.GetPropertyHeight(valueProp, label);
            
            return valueProp.CountInProperty() * base.GetPropertyHeight(valueProp, label) * 1.1f;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!property.objectReferenceValue)
            {
                EditorGUI.PropertyField(position, property, label, true);
                return;
            }

            SerializedObject targetObject = new(property.objectReferenceValue);
            SerializedProperty valueProp = targetObject.FindProperty("_value");
            
            if (valueProp != null)
            {
                const float buttonWidth = 20f;
                const float padding = 2f;

                Rect fieldPos = position;
                fieldPos.width -= 2 * padding + 2 * buttonWidth;

                Rect selectButtonPos = position;
                selectButtonPos.x += fieldPos.width + padding;
                selectButtonPos.width = buttonWidth;

                Rect removeButtonPos = selectButtonPos;
                removeButtonPos.x += buttonWidth + padding;
                
                EditorGUI.PropertyField(fieldPos, valueProp, label, true);
                
                GUIContent selectContent = new ("Sel", "Select underlying asset");
                GUIContent removeContent = new ("Del", "Clear Field");

                if (GUI.Button(selectButtonPos, selectContent, EditorStyles.miniButtonRight))
                {
                    EditorGUIUtility.PingObject( property.objectReferenceValue );
                }
                
                if (GUI.Button(removeButtonPos, removeContent, EditorStyles.miniButtonRight))
                {
                    property.objectReferenceValue = null;
                    property.serializedObject.ApplyModifiedProperties();
                }
            }
            else
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
            
        }
    }
}