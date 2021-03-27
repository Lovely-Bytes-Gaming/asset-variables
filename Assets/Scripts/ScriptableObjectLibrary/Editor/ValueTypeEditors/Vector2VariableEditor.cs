using UnityEditor;

[CustomEditor(typeof(Vector2Variable))]
public class Vector2VariableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Vector2Variable var = (Vector2Variable)target;
        var.Value = EditorGUILayout.Vector2Field("value: ", var.Value);
    }
}