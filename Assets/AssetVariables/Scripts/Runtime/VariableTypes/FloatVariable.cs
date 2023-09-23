using UnityEngine;


namespace LovelyBytesGaming.AssetVariables
{
    [CreateAssetMenu(menuName = "LovelyBytesGaming/AssetVariables/Float")]
    public class FloatVariable : VariableType<float> 
    {
        public void Reset() => Value = 0f;
    };
}
