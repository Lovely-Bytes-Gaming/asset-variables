using UnityEditor;

[CustomEditor(typeof(IntRange))]
public class IntRangeEditor : RangeTypeEditor<int>
{
    protected override int GenericEditorField(string description, int value)
        => EditorGUILayout.IntField(description, value);

    protected override int GenericSlider(string description, int value, int min, int max)
        => EditorGUILayout.IntSlider(description, value, min, max);
}