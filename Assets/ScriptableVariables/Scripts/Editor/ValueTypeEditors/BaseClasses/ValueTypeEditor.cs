using UnityEngine;
using UnityEditor;
using System;

namespace InflamedGums.DataManagement.ScriptableVariables
{
    public abstract class ValueTypeEditor<T> : Editor where T : struct, IEquatable<T>
    {
        protected abstract T GenericEditorField(string description, T value);

        public override void OnInspectorGUI()
        {
            ValueType<T> var = (ValueType<T>)target;
            var.Value = GenericEditorField("Value: ", var.Value);

            var.isLocked = EditorGUILayout.Toggle("Locked: ", var.isLocked);

            EditorGUILayout.Space(20f);
            if (GUILayout.Button("Invoke"))
                var.Invoke();

            if (GUI.changed) EditorUtility.SetDirty(var);
        }
    }
}
