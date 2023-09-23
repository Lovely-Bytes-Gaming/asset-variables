using UnityEditor;


namespace LovelyBytesGaming.AssetVariables
{
    [CustomEditor(typeof(LongVariable))]
    public class LongVariableEditor : VariableTypeEditor<long>
    {
        protected override long GenericEditorField(string description, long value)
            => EditorGUILayout.LongField(description, value);
    }
}