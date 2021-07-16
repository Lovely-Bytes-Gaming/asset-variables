
namespace CustomLibrary.Util.ScriptableVariables
{
    public class BitMask32Observer
    {
        public delegate void BitChangedEvent(bool value);
        /// <summary>
        /// Subscribe to this event to get notified when the bit at the position
        /// of this observer changes.
        /// </summary>
        public event BitChangedEvent OnBitChanged;
        public bool HasListener => OnBitChanged == null;

        private int _bitToWatch = 0;
        private bool _oldBit = false;
        private BitMask32Variable _bitMaskToWatch;

        /// <summary>
        /// Specifies which bit triggers the 'OnBitChanged' event of this class.
        /// Clamps the specified bit to the range of the bit mask (0-31)
        /// </summary>
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

        /// <summary>
        /// A reference to the bit mask variable this observer watches
        /// </summary>
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

        /// <summary>
        /// Expects a bit mask and a specific bit position to observe.
        /// </summary>
        public BitMask32Observer(BitMask32Variable bitMaskToWatch, int bitToWatch)
        {
            BitToWatch = bitToWatch;

            _bitMaskToWatch = bitMaskToWatch;
            _bitMaskToWatch.OnValueChanged += OnBitMaskChanged;
            _oldBit = BitMask32Variable.IsBitSet(bitMaskToWatch.Value, bitToWatch);
        }

        ~BitMask32Observer()
        {
            _bitMaskToWatch.OnValueChanged -= OnBitMaskChanged;
        }

        private void OnBitMaskChanged(uint newValue)
        {
            bool newBit = BitMask32Variable.IsBitSet(newValue, _bitToWatch);

            if (_oldBit != newBit) OnBitChanged?.Invoke(newBit);
            _oldBit = newBit;
        }
    }
}