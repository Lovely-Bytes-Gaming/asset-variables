using UnityEngine;
using System;


namespace CustomLibs.Util.ScriptableVariables
{
    public abstract class RangeType<T> : ScriptableObject where T : struct, IEquatable<T>, IComparable<T>
    {
        public delegate void ValueChangedEvent();
        public ValueChangedEvent OnValueChanged;

        [SerializeField]
        protected T m_Value;

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

        public T Value
        {
            get => m_Value;
            set
            {
                value = value.CompareTo(m_Min) < 0 ? m_Min : value;
                value = value.CompareTo(m_Max) > 0 ? m_Max : value;

                if (!value.Equals(m_Value))
                {
                    m_Value = value;
                    OnValueChanged?.Invoke();
                }
            }
        }
    }
}
