using UnityEngine;


namespace InflamedGums.Util.ScriptableVariables
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Value Types/Double")]
    public class DoubleVariable : ValueType<double> 
    { 
        public void Reset() => Value = 0.0;
    };
}
