namespace LovelyBytes.AssetVariables
{
    public class IntMatch : MatchListener<int>
    {
        protected override int Compare(int a, int b)
        {
            return a.CompareTo(b);
        }
    }
}
