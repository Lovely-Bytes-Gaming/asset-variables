using UnityEngine;


namespace LovelyBytes.AssetVariables
{
    [CreateAssetMenu(menuName = AssetVariableConstants.DefaultAssetPath + "Range/Float")]
    public class FloatRange : Range<float> 
    { 
        public void Reset()
        {
            Min = 0f;
            Max = 1f;
            Value = 0f;
        }
    }
}