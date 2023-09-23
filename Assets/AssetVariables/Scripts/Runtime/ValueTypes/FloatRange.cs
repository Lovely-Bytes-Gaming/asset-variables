using UnityEngine;


namespace LovelyBytesGaming.AssetVariables
{
    [CreateAssetMenu(menuName = "LovelyBytesGaming/AssetVariables/Range/Float")]
    public class FloatRange : RangeType<float> 
    { 
        public void Reset()
        {
            Min = 0f;
            Max = 1f;
            Value = 0f;
        }
    };
}