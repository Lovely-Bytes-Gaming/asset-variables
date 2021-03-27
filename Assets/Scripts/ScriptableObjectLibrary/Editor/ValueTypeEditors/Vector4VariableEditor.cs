using UnityEditor;

[CustomEditor(typeof(Vector4Variable))]
public class Vector4VariableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Vector4Variable var = (Vector4Variable)target;
        var.Value = EditorGUILayout.Vector4Field("value: ", var.Value);
    }
}