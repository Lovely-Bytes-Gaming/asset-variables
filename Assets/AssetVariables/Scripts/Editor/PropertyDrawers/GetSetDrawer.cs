using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace LovelyBytes.AssetVariables
{
    [CustomPropertyDrawer(typeof(GetSetAttribute))]
    public class GetSetDrawer : PropertyDrawer
    {
        private object _lastKnownValue;
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var getSetAttribute = attribute as GetSetAttribute;

            VisualElement container = new();
            PropertyField valueField = new(property);

            _lastKnownValue = property.boxedValue;
            
            
            valueField.TrackPropertyValue(property, changedProperty =>
            {
                object oldValue = _lastKnownValue;
                object newValue = changedProperty.boxedValue;
                _lastKnownValue = newValue;
                
                Debug.Log($"{oldValue} -> {newValue}");
                
                object parent = PropertyDrawerUtils.GetParentObject(
                    changedProperty.propertyPath, 
                    changedProperty.serializedObject.targetObject);
                
                System.Type type = parent.GetType();
                PropertyInfo propertyInfo = type.GetProperty(getSetAttribute!.Name);

                if (propertyInfo != null)
                {
                    fieldInfo.SetValue(parent, oldValue);
                    propertyInfo.SetValue(parent, newValue, null);
                    changedProperty.serializedObject.ApplyModifiedProperties();
                }
            });
            
            container.Add(valueField);
            return container;
        }
    }
}
