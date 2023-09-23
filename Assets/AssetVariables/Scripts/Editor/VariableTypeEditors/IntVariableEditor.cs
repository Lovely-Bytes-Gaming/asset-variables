using UnityEditor;


namespace LovelyBytesGaming.AssetVariables
{
    [CustomEditor(typeof(IntVariable))]
    public class IntVariableEditor : VariableEditor<int>
    {
        protected override int GenericEditorField(string description, int value)
            => EditorGUILayout.IntField(description, value);
    }
}