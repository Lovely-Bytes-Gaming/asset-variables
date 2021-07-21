using UnityEngine;
using UnityEditor;
using System;

namespace CustomLibrary.Util.ScriptableVariables
{
    public abstract class RangeTypeEditor<T> : Editor where T : struct, IEquatable<T>, IComparable<T>
    {
        protected abstract T GenericEditorField(string description, T value);
        protected abstract T GenericSlider(string description, T value, T min, T max);

        public override void OnInspectorGUI()
        {
            RangeType<T> var = (RangeType<T>)target;
            var.Min = GenericEditorField("Minimum: ", var.Min);
            var.Max = GenericEditorField("Maximum: ", var.Max);
            var.Value = GenericSlider("Value: ", var.Value, var.Min, var.Max);

            var.isLocked = EditorGUILayout.Toggle("Locked: ", var.isLocked);

            if (GUI.changed) EditorUtility.SetDirty(var);
        }
    }
}

