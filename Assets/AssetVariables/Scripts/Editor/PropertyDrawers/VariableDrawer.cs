
using UnityEditor;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    [CustomPropertyDrawer(typeof(Variable<>), useForChildren: true)]
    public class VariableDrawer : PropertyDrawer
    {
        private struct SelectionTracker
        {
            public Object ObjectReference;
            public double Timestamp;
        }

        private const float _doubleClickTimeout = 0.2f;
        private SelectionTracker _selection;
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!TryGetValueProperty(property, out SerializedProperty valueProperty))
                return base.GetPropertyHeight(property, label);
            
            if (!valueProperty.isExpanded)
                return base.GetPropertyHeight(valueProperty, label);
            
            return valueProperty.CountInProperty() * base.GetPropertyHeight(valueProperty, label) * 1.1f;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if(!TryGetValueProperty(property, out SerializedProperty valueProperty))
                DrawEmptyPropertyField(position, property, label);
            else
                DrawAssignedPropertyField(position, property, valueProperty, label);
        }

        private static bool TryGetValueProperty(SerializedProperty property, out SerializedProperty valueProperty)
        {
            valueProperty = null;
            
            if (!property.objectReferenceValue)
                return false;
            
            SerializedObject targetObject = new(property.objectReferenceValue);
            valueProperty = targetObject.FindProperty("_value");
            return valueProperty != null;
        }
        
        private static void DrawEmptyPropertyField(in Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property, label, true);
        }
        
        private void DrawAssignedPropertyField(in Rect position, SerializedProperty property, 
            SerializedProperty valueProperty, GUIContent label)
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
            
            EditorGUI.PropertyField(fieldPos, valueProperty, label, true);
            
            GUIContent selectContent = new ("Sel", "Select underlying asset");
            GUIContent removeContent = new ("Del", "Clear Field");

            if (GUI.Button(selectButtonPos, selectContent, EditorStyles.miniButtonRight))
                SelectAsset(property.objectReferenceValue);                    
            
            if (GUI.Button(removeButtonPos, removeContent, EditorStyles.miniButtonRight))
                ClearProperty(property);
        }
        
        private void SelectAsset(Object asset)
        {
            double ts = EditorApplication.timeSinceStartup;
            
            if (ReferenceEquals(_selection.ObjectReference, asset) &&
                ts - _selection.Timestamp < _doubleClickTimeout)
            {
                Selection.objects = new[] { asset };
            }
            
            _selection.Timestamp = ts;
            _selection.ObjectReference = asset;
            EditorGUIUtility.PingObject(asset);
        }

        private static void ClearProperty(SerializedProperty property)
        {
            property.objectReferenceValue = null;
            property.serializedObject.ApplyModifiedProperties();
        } 
    }
}