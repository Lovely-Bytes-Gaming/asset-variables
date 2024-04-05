using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    public abstract class Range<TType> : Variable<TType>
    {
        public TType Min
        {
            get => _min;
            set
            {
                _min = value;
                _max = Compare(value, _max) > 0 ? value : _max;
                ClampValue();
            }
        }

        public TType Max
        {
            get => _max;
            set
            {
                _max = value;
                _min = Compare(value, _min) < 0 ? value : _min;
                ClampValue();
            }
        }

        /// <summary>
        /// Clamp the new value to fall between Min and Max before setting it.
        /// </summary>
        protected override void OnBeforeSet(ref TType newValue)
        {
            newValue = Compare(newValue, Min) < 0 ? Min : newValue;
            newValue = Compare(newValue, Max) > 0 ? Max : newValue;
        }

        protected abstract int Compare(TType a, TType b);
        
        [SerializeField, HideInInspector]
        private TType _min;

        [SerializeField, HideInInspector]
        private TType _max;
        
        private void ClampValue()
        {
            if (Compare(Value, Max) > 0)
                Value = Max;
            else if (Compare(Value, Min) < 0)
                Value = Min;
        }
    }
}
