using UnityEngine;
using UnityEditor;
using System;

namespace LovelyBytes.AssetVariables
{
    public abstract class RangeEditor<TType> : Editor 
        where TType : IComparable<TType>
    {
        protected abstract TType GenericEditorField(string description, TType value);
        protected abstract TType GenericSlider(string description, TType value, TType min, TType max);
        protected abstract void ClampDefaultValue(SerializedProperty value, TType min, TType max);

        public override void OnInspectorGUI()
        {
            if (target is not Range<TType> rangeType)
                return;
            
            TType newMin = GenericEditorField("Minimum: ", rangeType.Min);
            TType newMax = GenericEditorField("Maximum: ", rangeType.Max);
            TType newValue = GenericSlider("Value: ", rangeType.Value, rangeType.Min, rangeType.Max);

            SerializedProperty defaultValue = serializedObject.FindProperty("_defaultValue");
            EditorGUILayout.PropertyField(defaultValue);

            if (!GUI.changed)
                return;

            rangeType.Min = newMin;
            rangeType.Max = newMax;
            rangeType.Value = newValue;
            
            SerializedProperty value = defaultValue.FindPropertyRelative("Value");
            ClampDefaultValue(value, newMin, newMax);

            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(rangeType);
        }
    }
}

