using UnityEngine;


namespace LovelyBytes.AssetVariables
{
    [CreateAssetMenu(menuName = AssetVariableConstants.DefaultAssetPath + "Range/Int")]
    public class IntRange : Range<int> 
    {
        public void Increment() => ++Value;
        public void Decrement() => --Value;

        public override void Lerp(float t)
        {
            Value = Mathf.RoundToInt(Mathf.Lerp(Min, Max, t));
        }

        public override float InverseLerp()
        {
            return Mathf.InverseLerp(Min, Max, Value);
        }
        
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