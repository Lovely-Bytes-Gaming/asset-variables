using UnityEditor;
using UnityEngine;


namespace InflamedGums.Util.ScriptableVariables
{
    [CustomEditor(typeof(Vector2Variable))]
    public class Vector2VariableEditor : ValueTypeEditor<Vector2>
    {
        protected override Vector2 GenericEditorField(string description, Vector2 value)
            => EditorGUILayout.Vector2Field(description, value);
    }
}