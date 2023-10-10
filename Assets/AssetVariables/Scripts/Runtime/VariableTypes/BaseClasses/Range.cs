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
        /// Current value of this instance, clamped to fall between Min and Max.
        /// </summary>
        protected override void SetValue(TType value)
        {
            value = value.CompareTo(Min) < 0 ? Min : value;
            value = value.CompareTo(Max) > 0 ? Max : value;
            base.Value = value;
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
