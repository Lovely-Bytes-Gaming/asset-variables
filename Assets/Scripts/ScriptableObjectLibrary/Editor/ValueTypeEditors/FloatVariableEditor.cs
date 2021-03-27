using UnityEditor;

[CustomEditor(typeof(FloatVariable))]
public class FloatVariableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        FloatVariable var = (FloatVariable)target;
        var.Value = EditorGUILayout.FloatField("value: ", var.Value);
    }
}