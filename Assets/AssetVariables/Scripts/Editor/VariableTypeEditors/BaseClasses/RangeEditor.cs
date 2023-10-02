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
            
            var value = (Range<TType>)target;
            
            TType newMin = GenericEditorField("Minimum: ", value.Min);
            TType newMax = GenericEditorField("Maximum: ", value.Max);
            TType newValue = GenericSlider("Value: ", value.Value, value.Min, value.Max);

            if (!GUI.changed)
                return;

            value.Min = newMin;
            value.Max = newMax;
            
            if (Application.isPlaying)
                value.Value = newValue;
            else
                value.SetWithoutNotify(newValue);
            
            EditorUtility.SetDirty(value);
        }
    }
}

