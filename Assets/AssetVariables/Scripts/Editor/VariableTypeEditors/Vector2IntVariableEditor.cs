using UnityEditor;
using UnityEngine;


namespace LovelyBytesGaming.AssetVariables
{
    [CustomEditor(typeof(Vector2IntVariable))]
    public class Vector2IntVariableEditor : VariableEditor<Vector2Int>
    {
        protected override Vector2Int GenericEditorField(string description, Vector2Int value)
            => EditorGUILayout.Vector2IntField(description, value);
    }
}