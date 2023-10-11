using UnityEngine;
using System;

namespace LovelyBytes.AssetVariables
{
    /// <summary>
    /// Base class for range types (int and float)
    /// Instances can be created in the Asset menu via
    /// Create -> Scriptable Objects -> Range Types -> desired type
    /// </summary>
    public abstract class Range<TType> : Variable<TType> where TType : IComparable<TType>
    {
        /// <summary>
        /// Minimum value of this instance
        /// </summary>
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

        /// <summary>
        /// Maximum value of this instance
        /// </summary>
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
