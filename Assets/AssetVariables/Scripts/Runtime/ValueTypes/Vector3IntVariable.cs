using UnityEngine;


namespace LovelyBytesGaming.AssetVariables
{
    [CreateAssetMenu(menuName = "LovelyBytesGaming/AssetVariables/Vector3Int")]
    public class Vector3IntVariable : ValueType<Vector3Int> 
    { 
        public void Reset() => Value = Vector3Int.zero;
    };
}