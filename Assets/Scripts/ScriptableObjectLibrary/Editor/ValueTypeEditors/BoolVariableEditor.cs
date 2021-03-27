using UnityEditor;

[CustomEditor(typeof(BoolVariable))]
public class BoolVariableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        BoolVariable var = (BoolVariable)target;
        var.Value = EditorGUILayout.Toggle("value: ", var.Value);
    }
}