using UnityEngine;
using System;

public abstract class RangeType<T> : ValueType<T> where T : struct, IEquatable<T>, IComparable<T>
{
    public T Min
    {
        get => m_Min;
        set
        {
            m_Min = value;
            m_Max = value.CompareTo(m_Max) > 0 ? value : m_Max;
            m_Value = m_Value.CompareTo(m_Min) < 0 ? m_Min : m_Value;
        }
    }

    public T Max
    {
        get => m_Max;
        set
        {
            m_Max = value;
            m_Min = value.CompareTo(m_Min) < 0 ? value : m_Min;
            m_Value = m_Value.CompareTo(m_Max) > 0 ? m_Max : m_Value;
        }
    }

    protected T m_Min;
    protected T m_Max;

    public override T Value
    {
        get => m_Value;
        set
        {
            value = value.CompareTo(m_Min) < 0 ? m_Min : value;
            value = value.CompareTo(m_Max) > 0 ? m_Max : value;
            base.Value = value;
        }
    }
}
