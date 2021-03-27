using UnityEditor;

[CustomEditor(typeof(IntRange))]
public class IntRangeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        IntRange var = (IntRange)target;
        var.Min = EditorGUILayout.IntField("Minimum: ", var.Min);
        var.Max = EditorGUILayout.IntField("Maximum: ", var.Max);
        var.Value = EditorGUILayout.IntSlider("Value: ", var.Value, var.Min, var.Max);
    }
}