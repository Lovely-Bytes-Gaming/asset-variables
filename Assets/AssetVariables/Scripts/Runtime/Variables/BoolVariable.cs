using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    [CreateAssetMenu(menuName = AssetVariableConstants.DefaultAssetPath + "Bool")]
    public class BoolVariable : Variable<bool>
    {
        public void Toggle()
        {
            Value = !Value;
        }
    }
}