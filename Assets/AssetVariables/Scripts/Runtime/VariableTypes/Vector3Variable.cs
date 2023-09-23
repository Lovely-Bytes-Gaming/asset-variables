using UnityEngine;


namespace LovelyBytesGaming.AssetVariables
{
    [CreateAssetMenu(menuName = "LovelyBytesGaming/AssetVariables/Vector3")]
    public class Vector3Variable : VariableType<Vector3> 
    { 
        public void Reset() => Value = Vector3.zero;
    };
}