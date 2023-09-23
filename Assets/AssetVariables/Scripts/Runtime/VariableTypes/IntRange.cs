using UnityEngine;


namespace LovelyBytesGaming.AssetVariables
{
    [CreateAssetMenu(menuName = "LovelyBytesGaming/AssetVariables/Range/Int")]
    public class IntRange : Range<int> 
    {
        public void Reset()
        {
            Min = 0;
            Max = 100;
            Value = 0;
        }
    };
}