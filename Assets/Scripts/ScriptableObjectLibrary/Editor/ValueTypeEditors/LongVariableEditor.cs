using UnityEditor;

[CustomEditor(typeof(LongVariable))]
public class LongVariableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        LongVariable var = (LongVariable)target;
        var.Value = EditorGUILayout.LongField("value: ", var.Value);
    }
}