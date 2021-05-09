using UnityEngine;
using UnityEditor;
using System;

namespace CustomLibs.Util.ScriptableVariables
{
    public abstract class ValueTypeEditor<T> : Editor where T : struct, IEquatable<T>
    {
        protected abstract T GenericEditorField(string description, T value);

        public override void OnInspectorGUI()
        {
            ValueType<T> var = (ValueType<T>)target;
            var.Value = GenericEditorField("Value: ", var.Value);

            if (GUI.changed) EditorUtility.SetDirty(var);
        }
    }
}
