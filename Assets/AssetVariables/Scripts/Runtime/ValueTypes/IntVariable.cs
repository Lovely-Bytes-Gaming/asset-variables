using UnityEngine;


namespace LovelyBytesGaming.AssetVariables
{
    [CreateAssetMenu(menuName = "LovelyBytesGaming/AssetVariables/Int")]
    public class IntVariable : ValueType<int> 
    { 
        public void Reset() => Value = 0;
    };
}
