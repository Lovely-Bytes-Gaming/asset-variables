namespace LovelyBytes.AssetVariables
{
    public interface IReadWriteView<TType>
    {
        TType Value { get; set; }
        event System.Action<TType, TType> OnValueChanged;
        void SetWithoutNotify(TType newValue);
    }
}
