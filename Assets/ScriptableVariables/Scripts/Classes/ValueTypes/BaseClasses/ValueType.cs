using UnityEngine;
using System;

namespace InflamedGums.Util.ScriptableVariables
{
    /// <summary>
    /// Base class for value types (Anything that is a struct)
    /// Instances can be created in the Asset menu via
    /// Create -> Scriptable Objects -> Value Types -> desired type
    /// </summary>
    public abstract class ValueType<T> : ScriptableObject where T : struct, IEquatable<T>
    {
        /// <summary>
        /// set to true if you don't want to allow the current value to be changed
        /// </summary>
        public bool isLocked =  false;

        /// <summary>
        /// Subscribe to this Event to get notified when the value of this object changes.
        /// Provides the new value as parameter.
        /// </summary>
        public event Action<T> OnValueChanged;

        public virtual T Value
        {
            get => m_Value;
            set
            {
                if (!value.Equals(m_Value) && !isLocked)
                {
                    m_Value = value;
                    Invoke();
                }
            }
        }

        /// <summary>
        /// Invoke this object's OnValueChanged event without actually changing it's value.
        /// </summary>
        public void Invoke()
            => OnValueChanged?.Invoke(m_Value);

        [SerializeField]
        protected T m_Value;
    }
}
