using UnityEngine;


namespace LovelyBytesGaming.AssetVariables
{
    [CreateAssetMenu(menuName = "LovelyBytesGaming/AssetVariables/Int")]
    public class IntVariable : Variable<int> 
    { 
        public void Reset() => Value = 0;
    };
}
