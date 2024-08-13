using UnityEditor;
using UnityEngine;


namespace LovelyBytes.AssetVariables
{
    [CustomEditor(typeof(FloatRange))]
    public class FloatRangeEditor : RangeEditor<float>
    {
        protected override float GenericEditorField(string description, float value)
            => EditorGUILayout.FloatField(description, value);

        protected override float GenericSlider(string description, float value, float min, float max)
            => EditorGUILayout.Slider(description, value, min, max);
    }
}