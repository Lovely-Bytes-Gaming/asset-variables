using UnityEditor;

[CustomEditor(typeof(Vector3IntVariable))]
public class Vector3IntVariableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Vector3IntVariable var = (Vector3IntVariable)target;
        var.Value = EditorGUILayout.Vector3IntField("value: ", var.Value);
    }
}