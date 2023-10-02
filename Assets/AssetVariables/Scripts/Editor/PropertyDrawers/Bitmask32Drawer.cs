using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace LovelyBytesGaming.AssetVariables
{
    [CustomPropertyDrawer(typeof(BitMask32))]
    public class Bitmask32Drawer : PropertyDrawer
    {
        private static readonly string[] labels = 
            Enumerable
            .Range(0, 32)
            .Select(x => x.ToString())
            .ToArray();
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            Object target = property.serializedObject.targetObject;
            
            var oldValue = (BitMask32)fieldInfo.GetValue(target);
            BitMask32 newValue = EditorGUI.MaskField(position, label, oldValue, labels); 
            
            fieldInfo.SetValue(target, newValue);
            
            EditorGUI.EndProperty();
        }
    }
}
