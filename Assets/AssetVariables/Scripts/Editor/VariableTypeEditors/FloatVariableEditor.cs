using UnityEditor;


namespace LovelyBytesGaming.AssetVariables
{
    [CustomEditor(typeof(FloatVariable))]
    public class FloatVariableEditor : VariableTypeEditor<float>
    {
        protected override float GenericEditorField(string description, float value)
            => EditorGUILayout.FloatField(description, value);
    }
}