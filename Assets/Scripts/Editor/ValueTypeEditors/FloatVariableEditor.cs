using UnityEditor;


namespace CustomLibs.Util.ScriptableVariables
{
    [CustomEditor(typeof(FloatVariable))]
    public class FloatVariableEditor : ValueTypeEditor<float>
    {
        protected override float GenericEditorField(string description, float value)
            => EditorGUILayout.FloatField(description, value);
    }
}