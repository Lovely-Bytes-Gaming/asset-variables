using UnityEngine;


namespace LovelyBytesGaming.AssetVariables
{
    [CreateAssetMenu(menuName = "LovelyBytesGaming/AssetVariables/Vector4")]
    public class Vector4Variable : VariableType<Vector4> 
    { 
        public void Reset() => Value = Vector4.zero;
    };
}
