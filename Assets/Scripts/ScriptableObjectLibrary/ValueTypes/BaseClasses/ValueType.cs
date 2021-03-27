using UnityEngine;
using System;

public abstract class ValueType<T> : ScriptableObject where T : struct, IEquatable<T>
{
    public delegate void ValueChangedEvent(T newValue);
    public ValueChangedEvent OnValueChanged;

    public virtual T Value
    {
        get => m_Value;
        set
        {
            if (!value.Equals(m_Value))
            {
                m_Value = value;
                OnValueChanged?.Invoke(m_Value);
            }
        }
    }
    
    protected T m_Value;
}
