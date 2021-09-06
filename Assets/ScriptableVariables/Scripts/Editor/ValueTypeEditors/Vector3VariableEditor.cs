using UnityEditor;
using UnityEngine;


namespace InflamedGums.DataManagement.ScriptableVariables
{
    [CustomEditor(typeof(Vector3Variable))]
    public class Vector3VariableEditor : ValueTypeEditor<Vector3>
    {
        protected override Vector3 GenericEditorField(string description, Vector3 value)
            => EditorGUILayout.Vector3Field(description, value);
    }
}