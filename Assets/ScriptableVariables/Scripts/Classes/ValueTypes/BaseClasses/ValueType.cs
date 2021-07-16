using UnityEngine;
using System;


namespace CustomLibrary.Util.ScriptableVariables
{
    public abstract class ValueType<T> : ScriptableObject where T : struct, IEquatable<T>
    {
        public delegate void ValueChangedEvent(T oldValue, T newValue);
        public ValueChangedEvent OnValueChanged;

        public T Value
        {
            get => m_Value;
            set
            {
                if (!value.Equals(m_Value))
                {
                    T tmp = m_Value;
                    m_Value = value;
                    OnValueChanged?.Invoke(tmp, value);
                }
            }
        }
    
        [SerializeField]
        protected T m_Value;
    }
}
