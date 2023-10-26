namespace LovelyBytes.AssetVariables
{
    public interface IReadWriteView<TType>
    {
        TType Value { get; set; }
        void SetWithoutNotify(TType newValue);
        event System.Action<TType, TType> OnValueChanged;
    }
}
