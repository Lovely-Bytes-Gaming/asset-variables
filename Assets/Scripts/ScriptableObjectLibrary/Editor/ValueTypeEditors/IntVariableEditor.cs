using UnityEditor;

[CustomEditor(typeof(IntVariable))]
public class IntVariableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        IntVariable var = (IntVariable)target;
        var.Value = EditorGUILayout.IntField("value: ", var.Value);
    }
}