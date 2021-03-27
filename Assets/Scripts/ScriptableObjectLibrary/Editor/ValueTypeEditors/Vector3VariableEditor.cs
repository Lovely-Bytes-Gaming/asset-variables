using UnityEditor;

[CustomEditor(typeof(Vector3Variable))]
public class Vector3VariableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Vector3Variable var = (Vector3Variable)target;
        var.Value = EditorGUILayout.Vector3Field("value: ", var.Value);
    }
}