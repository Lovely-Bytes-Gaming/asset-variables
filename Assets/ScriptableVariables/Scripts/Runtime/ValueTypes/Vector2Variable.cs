using UnityEngine;


namespace InflamedGums.Util.ScriptableVariables
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Value Types/Vector2")]
    public class Vector2Variable : ValueType<Vector2> 
    { 
        public void Reset() => Value = Vector2.zero;
    };
}
