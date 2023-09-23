using UnityEditor;
using UnityEngine;


namespace LovelyBytesGaming.AssetVariables
{
    [CustomEditor(typeof(Vector3IntVariable))]
    public class Vector3IntVariableEditor : VariableTypeEditor<Vector3Int>
    {
        protected override Vector3Int GenericEditorField(string description, Vector3Int value)
            => EditorGUILayout.Vector3IntField(description, value);
    }
}