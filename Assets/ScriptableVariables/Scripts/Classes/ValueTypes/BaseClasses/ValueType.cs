using UnityEngine;
using System;

namespace InflamedGums.DataManagement.ScriptableVariables
{
    /// <summary>
    /// Base class for value types (Anything that is a struct)
    /// Instances can be created in the Asset menu via
    /// Create -> Scriptable Objects -> Value Types -> desired type
    /// </summary>
    public abstract class ValueType<T> : ScriptableObject where T : struct, IEquatable<T>
    {
        /// <summary>
        /// You can optionally define a function here that checks whether this value can be edited.
        /// </summary>
        public bool isLocked =  false;

        public delegate void ValueChangedEvent(T newValue);
        /// <summary>
        /// Subscribe to this Event to get notified when the value of this object changes.
        /// Provides the new value as parameter.
        /// </summary>
        public event ValueChangedEvent OnValueChanged;

        public T Value
        {
            get => m_Value;
            set
            {
                if (!value.Equals(m_Value) && !isLocked)
                {
                    m_Value = value;
                    OnValueChanged?.Invoke(value);
                }
            }
        }
    
        [SerializeField]
        protected T m_Value;
    }
}
