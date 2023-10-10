using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    /// <summary>
    /// Base class for scriptable objects that wrap a given type.
    /// </summary>
    public abstract class Variable<TType> : ScriptableObject
    {
        /// <summary>
        /// Subscribe to this Event to get notified when the value of this object changes.
        /// Provides the old and new values as parameters.
        /// </summary>
        public event System.Action<TType, TType> OnValueChanged;

        private readonly object _lockObject = new(); 
        
        public TType Value
        {
            get
            {
                lock (_lockObject)
                {
                    return _value;
                }
            }
            set
            {
                lock (_lockObject)
                {
                    SetValue(value);
                }
            }
        }

        protected virtual void SetValue(TType value)
        {
            TType oldValue = _value;
            _value = value;
            OnValueChanged?.Invoke(oldValue, _value);
        }

        public void SetWithoutNotify(TType newValue)
        {
            lock (_lockObject)
            {
                _value = newValue;
            }
        }
        
        [SerializeField, GetSet(nameof(Value))]
        private TType _value;
    }
}
