using UnityEngine;


namespace InflamedGums.Util.ScriptableVariables
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Value Types/Bool")]
    public class BoolVariable : ValueType<bool> {
       public void Reset() => Value = false;
    };
}