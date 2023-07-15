using UnityEngine;
using System;

namespace InflamedGums.Util.ScriptableVariables
{
    /// <summary>
    /// Base class for enum type variables.
    /// Implementation-scripts of this class can be created via the editor:
    /// Window -> Scriptable Variables -> Create new Enum type.
    /// Instances can be created via the Right Click menu:
    /// Create -> Scriptable Variables -> Enum Types -> Your Type
    /// </summary>
    public abstract class EnumType<T> : ScriptableObject, IEnumType where T : Enum
    {
        public delegate void ValueChangedEvent(T newValue);
        /// <summary>
        /// Subscribe to this Event to get notified when the value of this object changes.
        /// Provides the new value as parameter.
        /// </summary>
        public event ValueChangedEvent OnValueChanged;
        /// <summary>
        /// You can optionally define a function here that checks whether this value can be edited.
        /// </summary>
        public bool isLocked =  false;

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

        public int ToInt()
            => Convert.ToInt32(Value);

        /// <summary>
        /// Invoke this object's OnValueChanged event without actually changing it's value.
        /// </summary>
        public void Invoke()
            => OnValueChanged?.Invoke(m_Value);

        [SerializeField]
        protected T m_Value;
    }
}