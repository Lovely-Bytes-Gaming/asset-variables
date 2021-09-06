using UnityEditor;
using UnityEngine;


namespace InflamedGums.Util.ScriptableVariables
{
    [CustomEditor(typeof(DoubleVariable))]
    public class DoubleVariableEditor : ValueTypeEditor<double>
    {
        protected override double GenericEditorField(string description, double value)
            => EditorGUILayout.DoubleField(description, value);
    }
}