using UnityEngine;


namespace InflamedGums.DataManagement.ScriptableVariables
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Value Types/Bool")]
    public class BoolVariable : ValueType<bool> {
       public void Reset() => Value = false;
    };
}