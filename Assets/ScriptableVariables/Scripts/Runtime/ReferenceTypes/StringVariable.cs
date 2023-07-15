using UnityEngine;
using System;

namespace InflamedGums.Util.ScriptableVariables
{
    /// <summary>
    /// Scriptable Variable implementation for strings
    /// </summary>
    [CreateAssetMenu(menuName = "Scriptable Objects/Reference Types/String")]
    public class StringVariable : ScriptableObject
    {
        public delegate void ValueChangedEvent(string newValue);
        /// <summary>
        /// Subscribe to this Event to get notified when the value of this object changes.
        /// Provides the new value as parameter.
        /// </summary>
        public event ValueChangedEvent OnValueChanged;
        /// <summary>
        /// You can optionally define a function here that checks whether this value can be edited.
        /// </summary>
        public bool isLocked =  false;

        public string Value
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

        public void Reset()
        {
            Value = "";
        }

        [SerializeField]
        private string m_Value;
    }
}