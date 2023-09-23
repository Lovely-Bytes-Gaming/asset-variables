using UnityEngine;


namespace LovelyBytesGaming.AssetVariables
{
    [CreateAssetMenu(menuName = "LovelyBytesGaming/AssetVariables/Vector2Int")]
    public class Vector2IntVariable : VariableType<Vector2Int> 
    { 
        public void Reset() => Value = Vector2Int.zero;
    };
}
