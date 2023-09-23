using UnityEditor;
using UnityEngine;


namespace LovelyBytesGaming.AssetVariables
{
    [CustomEditor(typeof(DoubleVariable))]
    public class DoubleVariableEditor : VariableTypeEditor<double>
    {
        protected override double GenericEditorField(string description, double value)
            => EditorGUILayout.DoubleField(description, value);
    }
}