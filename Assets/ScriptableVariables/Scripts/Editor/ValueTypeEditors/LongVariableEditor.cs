using UnityEditor;


namespace CustomLibrary.Util.ScriptableVariables
{
    [CustomEditor(typeof(LongVariable))]
    public class LongVariableEditor : ValueTypeEditor<long>
    {
        protected override long GenericEditorField(string description, long value)
            => EditorGUILayout.LongField(description, value);
    }
}