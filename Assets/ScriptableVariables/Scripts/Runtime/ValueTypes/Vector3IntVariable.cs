using UnityEngine;


namespace InflamedGums.Util.ScriptableVariables
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Value Types/Vector3 Int")]
    public class Vector3IntVariable : ValueType<Vector3Int> 
    { 
        public void Reset() => Value = Vector3Int.zero;
    };
}