using System.Collections.Generic;


namespace CustomLibs.Util.ScriptableVariables
{
    public class BitMask32Observer
    {
        public delegate void BitChangedEvent(bool value);
        public BitChangedEvent OnBitChanged;

        private int _bitToWatch = 0;
        private BitMask32Variable _bitMaskToWatch;

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

        public BitMask32Variable BitMaskToWatch
        {
            get => _bitMaskToWatch;
            set
            {
                _bitMaskToWatch.OnValueChanged -= OnBitMaskChanged;
                _bitMaskToWatch = value;

                _bitMaskToWatch.OnValueChanged += OnBitMaskChanged;
            }
        }

        public BitMask32Observer(BitMask32Variable bitMaskToWatch, int bitToWatch)
        {
            _bitMaskToWatch = bitMaskToWatch;
            _bitMaskToWatch.OnValueChanged += OnBitMaskChanged;
            BitToWatch = bitToWatch;
        }

        ~BitMask32Observer()
        {
            _bitMaskToWatch.OnValueChanged -= OnBitMaskChanged;
        }

        private void OnBitMaskChanged(uint oldValue, uint newValue)
        {
            bool oldBit = BitMask32Variable.GetBit(oldValue, _bitToWatch);
            bool newBit = BitMask32Variable.GetBit(newValue, _bitToWatch);

            if (oldBit != newBit) OnBitChanged?.Invoke(newBit);
        }
    }
}