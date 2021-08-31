using UnityEditor;


namespace InflamedGums.DataManagement.ScriptableVariables
{
    [CustomEditor(typeof(IntVariable))]
    public class IntVariableEditor : ValueTypeEditor<int>
    {
        protected override int GenericEditorField(string description, int value)
            => EditorGUILayout.IntField(description, value);
    }
}