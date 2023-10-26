using UnityEngine;
using System;

namespace LovelyBytes.AssetVariables
{
    public abstract class Range<TType> : Variable<TType> where TType : IComparable<TType>
    {
        public TType Min
        {
            get => _min;
            set
            {
                _min = value;
                _max = value.CompareTo(_max) > 0 ? value : _max;
                ClampValue();
            }
        }

        public TType Max
        {
            get => _max;
            set
            {
                _max = value;
                _min = value.CompareTo(_min) < 0 ? value : _min;
                ClampValue();
            }
        }

        /// <summary>
        /// Clamp the new value to fall between Min and Max before setting it.
        /// </summary>
        protected override void OnBeforeSet(ref TType newValue)
        {
            newValue = newValue.CompareTo(Min) < 0 ? Min : newValue;
            newValue = newValue.CompareTo(Max) > 0 ? Max : newValue;
        }

        [SerializeField, HideInInspector]
        private TType _min;

        [SerializeField, HideInInspector]
        private TType _max;
        
        private void ClampValue()
        {
            if (Value.CompareTo(Max) > 0)
                Value = Max;
            else if (Value.CompareTo(Min) < 0)
                Value = Min;
        }
        
        
    }
}
