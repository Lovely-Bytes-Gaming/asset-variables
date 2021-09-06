using UnityEngine;


namespace InflamedGums.DataManagement.ScriptableVariables
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Value Types/Float")]
    public class FloatVariable : ValueType<float> 
    {
        public void Reset() => Value = 0f;
    };
}
