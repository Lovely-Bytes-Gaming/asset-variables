using UnityEngine;


namespace LovelyBytes.AssetVariables
{
    [CreateAssetMenu(menuName = AssetVariableConstants.DefaultAssetPath + "Int")]
    public class IntVariable : Variable<int>
    {
        public void Increment() => ++Value;
        public void Decrement() => --Value;
    }
}
