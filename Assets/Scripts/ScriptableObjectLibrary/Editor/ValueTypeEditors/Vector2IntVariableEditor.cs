using UnityEditor;

[CustomEditor(typeof(Vector2IntVariable))]
public class Vector2IntVariableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Vector2IntVariable var = (Vector2IntVariable)target;
        var.Value = EditorGUILayout.Vector2IntField("value: ", var.Value);
    }
}