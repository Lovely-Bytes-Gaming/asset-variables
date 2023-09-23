using UnityEditor;

namespace LovelyBytesGaming.AssetVariables
{
    [CustomEditor(typeof(BoolVariable))]
    public class BoolVariableEditor : VariableEditor<bool>
    {
        protected override bool GenericEditorField(string description, bool value)
            => EditorGUILayout.Toggle(description, value);
    }
}
