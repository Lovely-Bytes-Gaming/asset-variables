using UnityEngine;


namespace LovelyBytes.AssetVariables
{
    [CreateAssetMenu(menuName = AssetVariableConstants.DefaultAssetPath + "Range/Int")]
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