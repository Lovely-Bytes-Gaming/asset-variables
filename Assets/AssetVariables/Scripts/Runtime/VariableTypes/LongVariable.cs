using UnityEngine;


namespace LovelyBytesGaming.AssetVariables
{
    [CreateAssetMenu(menuName = "LovelyBytesGaming/AssetVariables/Long")]
    public class LongVariable : VariableType<long> 
    {
        public void Reset() => Value = 0;
    };
}