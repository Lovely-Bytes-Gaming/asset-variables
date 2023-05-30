using UnityEngine;


namespace InflamedGums.Util.ScriptableVariables
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Value Types/Long")]
    public class LongVariable : ValueType<long> 
    {
        public void Reset() => Value = 0;
    };
}