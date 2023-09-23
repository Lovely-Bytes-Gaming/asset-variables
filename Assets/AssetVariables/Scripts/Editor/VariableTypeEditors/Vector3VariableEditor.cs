using UnityEditor;
using UnityEngine;


namespace LovelyBytesGaming.AssetVariables
{
    [CustomEditor(typeof(Vector3Variable))]
    public class Vector3VariableEditor : VariableTypeEditor<Vector3>
    {
        protected override Vector3 GenericEditorField(string description, Vector3 value)
            => EditorGUILayout.Vector3Field(description, value);
    }
}