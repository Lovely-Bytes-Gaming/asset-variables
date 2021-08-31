using UnityEngine;


namespace InflamedGums.DataManagement.ScriptableVariables
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Value Types/Quaternion")]
    public class QuaternionVariable : ValueType<Quaternion> 
    { 
        public void Reset() => Value = Quaternion.identity;
    }
}