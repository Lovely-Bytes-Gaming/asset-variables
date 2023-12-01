namespace LovelyBytes.AssetVariables
{
    public interface IReadWriteView<TType> : IReadOnlyView<TType>
    {
        void SetWithoutNotify(TType newValue);
    }
}
