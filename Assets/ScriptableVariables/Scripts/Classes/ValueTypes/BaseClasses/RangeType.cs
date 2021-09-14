using UnityEngine;
using System;


namespace InflamedGums.Util.ScriptableVariables
{
    /// <summary>
    /// Base class for range types (int and float)
    /// Instances can be created in the Asset menu via
    /// Create -> Scriptable Objects -> Range Types -> desired type
    /// </summary>
    public abstract class RangeType<T> : ValueType<T> where T : struct, IEquatable<T>, IComparable<T>
    {
        /// <summary>
        /// Minimum value of this instance
        /// </summary>
        public T Min
        {
            get => m_Min;
            set
            {
                m_Min = value;
                m_Max = value.CompareTo(m_Max) > 0 ? value : m_Max;
                Value = m_Value.CompareTo(m_Min) < 0 ? m_Min : m_Value;
            }
        }

        /// <summary>
        /// Maximum value of this instance
        /// </summary>
        public T Max
        {
            get => m_Max;
            set
            {
                m_Max = value;
                m_Min = value.CompareTo(m_Min) < 0 ? value : m_Min;
                Value = m_Value.CompareTo(m_Max) > 0 ? m_Max : m_Value;
            }
        }

        [SerializeField]
        protected T m_Min, m_Max;

        /// <summary>
        /// Current value of this instance, clamped to fall between Min and Max.
        /// </summary>
        public override T Value
        {
            get => base.Value;
            set
            {
                value = value.CompareTo(m_Min) < 0 ? m_Min : value;
                value = value.CompareTo(m_Max) > 0 ? m_Max : value;

                if (!value.Equals(m_Value) && !isLocked)
                {
                    m_Value = value;
                    Invoke();
                }
            }
        }
    }
}
