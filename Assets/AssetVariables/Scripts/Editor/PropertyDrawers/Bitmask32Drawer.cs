using System.Linq;
using UnityEditor;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    [CustomPropertyDrawer(typeof(BitMask32))]
    public class Bitmask32Drawer : PropertyDrawer
    {
        private static readonly string[] _labels = 
            Enumerable
            .Range(0, 32)
            .Select(x => x.ToString())
            .ToArray();
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            Object target = property.serializedObject.targetObject;
            
            BitMask32 oldValue = (BitMask32)fieldInfo.GetValue(target);
            BitMask32 newValue = EditorGUI.MaskField(position, label, oldValue, _labels); 
            
            fieldInfo.SetValue(target, newValue);
            
            EditorGUI.EndProperty();
        }
    }
}
