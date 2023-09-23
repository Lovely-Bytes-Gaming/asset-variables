using UnityEngine;


namespace LovelyBytesGaming.AssetVariables
{
    [CreateAssetMenu(menuName = "LovelyBytesGaming/AssetVariables/Int")]
    public class IntVariable : VariableType<int> 
    { 
        public void Reset() => Value = 0;
    };
}
