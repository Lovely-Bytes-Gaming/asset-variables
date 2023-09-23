using UnityEditor;
using UnityEngine;


namespace LovelyBytesGaming.AssetVariables
{
    [CustomEditor(typeof(Vector4Variable))]
    public class Vector4VariableEditor : VariableTypeEditor<Vector4>
    {
        protected override Vector4 GenericEditorField(string description, Vector4 value)
            => EditorGUILayout.Vector4Field(description, value);
    }
}