using UnityEngine;


namespace LovelyBytes.AssetVariables
{
    [CreateAssetMenu(menuName = AssetVariableConstants.DefaultAssetPath + "Range/Int")]
    public class IntRange : Range<int> 
    {
        public void Increment() => ++Value;
        public void Decrement() => --Value;
        
        public void Reset()
        {
            Min = 0;
            Max = 100;
            Value = 0;
        }

        protected override int Compare(int a, int b)
        {
            return a.CompareTo(b);
        }
    };
}