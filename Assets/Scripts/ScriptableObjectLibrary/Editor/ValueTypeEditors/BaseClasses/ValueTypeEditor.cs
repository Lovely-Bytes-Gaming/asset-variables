using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

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
