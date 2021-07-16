using UnityEngine;


namespace CustomLibrary.Util.ScriptableVariables
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Value Types/Double")]
    public class DoubleVariable : ValueType<double> 
    { 
        void Reset() => m_Value = 0.0;
    };
}
