
namespace CustomLibs.Util.ScriptableVariables
{
    public class BitMask16Observer
    {
        public delegate void BitChangedEvent(bool value);
        public BitChangedEvent OnBitChanged;

        private int _bitToWatch = 0;
        private BitMask16Variable _bitMaskToWatch;

        public int BitToWatch
        {
            get => _bitToWatch;
            set
            {
                if (value != _bitToWatch)
                {
                    _bitToWatch = value;
                    _bitToWatch = (_bitToWatch > 15) ? 15 : _bitToWatch;
                    _bitToWatch = (_bitToWatch < 0) ? 0 : _bitToWatch;
                }
            }
        }

        public BitMask16Variable BitMaskToWatch
        {
            get => _bitMaskToWatch;
            set
            {
                _bitMaskToWatch.OnValueChanged -= OnBitMaskChanged;
                _bitMaskToWatch = value;

                _bitMaskToWatch.OnValueChanged += OnBitMaskChanged;
            }
        }

        public BitMask16Observer(BitMask16Variable bitMaskToWatch, int bitToWatch)
        {
            _bitMaskToWatch = bitMaskToWatch;
            _bitMaskToWatch.OnValueChanged += OnBitMaskChanged;
            BitToWatch = bitToWatch;
        }

        ~BitMask16Observer()
        {
            _bitMaskToWatch.OnValueChanged -= OnBitMaskChanged;
        }

        private void OnBitMaskChanged(ushort oldValue, ushort newValue)
        {
            bool oldBit = BitMask16Variable.GetBit(oldValue, _bitToWatch);
            bool newBit = BitMask16Variable.GetBit(newValue, _bitToWatch);

            if (oldBit != newBit) OnBitChanged?.Invoke(newBit);
        }
    }
}