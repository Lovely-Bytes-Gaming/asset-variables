using UnityEngine;


namespace LovelyBytesGaming.AssetVariables
{
    [CreateAssetMenu(menuName = "LovelyBytesGaming/AssetVariables/Double")]
    public class DoubleVariable : Variable<double> 
    { 
        public void Reset() => Value = 0.0;
    };
}
