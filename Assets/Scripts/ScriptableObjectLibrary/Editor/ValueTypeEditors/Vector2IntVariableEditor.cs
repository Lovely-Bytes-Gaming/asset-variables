using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Vector2IntVariable))]
public class Vector2IntVariableEditor : ValueTypeEditor<Vector2Int>
{
    protected override Vector2Int GenericEditorField(string description, Vector2Int value)
        => EditorGUILayout.Vector2IntField(description, value);
}