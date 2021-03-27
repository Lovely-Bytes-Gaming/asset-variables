using UnityEditor;

[CustomEditor(typeof(DoubleVariable))]
public class DoubleVariableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DoubleVariable var = (DoubleVariable)target;
        var.Value = EditorGUILayout.DoubleField("value: ", var.Value);
    }
}