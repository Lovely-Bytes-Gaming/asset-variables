using UnityEditor;
using UnityEngine;


namespace LovelyBytesGaming.AssetVariables
{
    [CustomEditor(typeof(Vector2Variable))]
    public class Vector2VariableEditor : VariableEditor<Vector2>
    {
        protected override Vector2 GenericEditorField(string description, Vector2 value)
            => EditorGUILayout.Vector2Field(description, value);
    }
}