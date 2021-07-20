using UnityEngine;


namespace CustomLibrary.Util.ScriptableVariables
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Value Types/Vector2 Int")]
    public class Vector2IntVariable : ValueType<Vector2Int> 
    { 
        public void Reset() => Value = Vector2Int.zero;
    };
}
