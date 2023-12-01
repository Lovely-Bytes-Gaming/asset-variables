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

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (target is not Range<TType> rangeType)
                return;
            
            TType newMin = GenericEditorField("Minimum: ", rangeType.Min);
            TType newMax = GenericEditorField("Maximum: ", rangeType.Max);
            TType newValue = GenericSlider("Value: ", rangeType.Value, rangeType.Min, rangeType.Max);

            if (!GUI.changed)
                return;

            rangeType.Min = newMin;
            rangeType.Max = newMax;
            
            if (Application.isPlaying)
                rangeType.Value = newValue;
            else
                rangeType.SetWithoutNotify(newValue);
            
            EditorUtility.SetDirty(rangeType);
        }
    }
}

