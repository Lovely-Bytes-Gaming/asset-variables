using UnityEngine;
using System;

namespace LovelyBytesGaming.AssetVariables
{
    /// <summary>
    /// Base class for range types (int and float)
    /// Instances can be created in the Asset menu via
    /// Create -> Scriptable Objects -> Range Types -> desired type
    /// </summary>
    public abstract class RangeType<TType> : VariableType<TType> where TType : IComparable<TType>
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

        [SerializeField]
        private TType _min;

        [SerializeField]
        private TType _max;

        /// <summary>
        /// Current value of this instance, clamped to fall between Min and Max.
        /// </summary>
        public override TType Value
        {
            get => base.Value;
            set
            {
                value = value.CompareTo(Min) < 0 ? Min : value;
                value = value.CompareTo(Max) > 0 ? Max : value;
                base.Value = value;
            }
        }

        public override void SetWithoutNotify(TType newValue)
        {
            if (newValue.CompareTo(Max) > 0)
                base.SetWithoutNotify(Max);
            else if (newValue.CompareTo(Min) < 0)
                base.SetWithoutNotify(Min);
            else
                base.SetWithoutNotify(newValue);
        }

        private void ClampValue()
        {
            if (Value.CompareTo(Max) > 0)
                Value = Max;
            else if (Value.CompareTo(Min) < 0)
                Value = Min;
        }
    }
}
