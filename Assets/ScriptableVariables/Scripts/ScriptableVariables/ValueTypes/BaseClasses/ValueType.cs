using UnityEngine;
using System;


namespace CustomLibs.Util.ScriptableVariables
{
    public abstract class ValueType<T> : ScriptableObject where T : struct, IEquatable<T>
    {
        public delegate void ValueChangedEvent();
        public ValueChangedEvent OnValueChanged;

        public T Value
        {
            get => m_Value;
            set
            {
                if (!value.Equals(m_Value))
                {
                    m_Value = value;
                    OnValueChanged?.Invoke();
                }
            }
        }
    
        [SerializeField]
        protected T m_Value;
    }
}
