using UnityEngine;


namespace InflamedGums.Util.ScriptableVariables
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Value Types/Quaternion")]
    public class QuaternionVariable : ValueType<Quaternion> 
    { 
        public void Reset() => Value = Quaternion.identity;
    }
}