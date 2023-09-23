using UnityEngine;

namespace LovelyBytesGaming.AssetVariables
{
    [CreateAssetMenu(menuName = "LovelyBytesGaming/AssetVariables/Bool")]
    public class BoolVariable : VariableType<bool> {
       public void Reset() => Value = false;
    };
}