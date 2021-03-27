using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Vector4Variable))]
public class Vector4VariableEditor : ValueTypeEditor<Vector4>
{
    protected override Vector4 GenericEditorField(string description, Vector4 value)
        => EditorGUILayout.Vector4Field(description, value);
}