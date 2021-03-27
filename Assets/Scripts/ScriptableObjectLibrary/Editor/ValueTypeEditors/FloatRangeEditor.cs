using UnityEditor;

[CustomEditor(typeof(FloatRange))]
public class FloatRangeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        FloatRange var = (FloatRange)target;
        var.Min = EditorGUILayout.FloatField("Minimum: ", var.Min);
        var.Max = EditorGUILayout.FloatField("Maximum: ", var.Max);
        var.Value = EditorGUILayout.Slider("Value: ", var.Value, var.Min, var.Max);
    }
}