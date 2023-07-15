using UnityEngine;


namespace InflamedGums.Util.ScriptableVariables
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Value Types/Int")]
    public class IntVariable : ValueType<int> 
    { 
        public void Reset() => Value = 0;

    };
}