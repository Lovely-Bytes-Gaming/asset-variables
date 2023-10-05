using UnityEditor;


namespace LovelyBytes.AssetVariables
{
    [CustomEditor(typeof(IntRange))]
    public class IntRangeEditor : RangeEditor<int>
    {
        protected override int GenericEditorField(string description, int value)
            => EditorGUILayout.IntField(description, value);

        protected override int GenericSlider(string description, int value, int min, int max)
            => EditorGUILayout.IntSlider(description, value, min, max);
    }
}