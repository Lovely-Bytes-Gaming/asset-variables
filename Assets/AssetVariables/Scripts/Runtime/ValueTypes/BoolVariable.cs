using UnityEngine;

namespace LovelyBytesGaming.AssetVariables
{
    [CreateAssetMenu(menuName = "LovelyBytesGaming/AssetVariables/Bool")]
    public class BoolVariable : ValueType<bool> {
       public void Reset() => Value = false;
    };
}