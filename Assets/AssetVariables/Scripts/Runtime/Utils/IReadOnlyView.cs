namespace LovelyBytes.AssetVariables
{
    public interface IReadOnlyView<out TType> 
    {
        TType Value { get; }
        event System.Action<TType, TType> OnValueChanged;
    }
}
