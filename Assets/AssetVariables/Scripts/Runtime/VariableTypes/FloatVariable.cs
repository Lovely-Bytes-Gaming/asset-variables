using UnityEngine;


namespace LovelyBytesGaming.AssetVariables
{
    [CreateAssetMenu(menuName = "LovelyBytesGaming/AssetVariables/Float")]
    public class FloatVariable : Variable<float> 
    {
        public void Reset() => Value = 0f;
    };
}
