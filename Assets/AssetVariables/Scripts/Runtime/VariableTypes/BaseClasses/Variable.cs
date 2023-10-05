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

        public virtual TType Value
        {
            get => _value;
            set
            {
                TType oldValue = _value;
                _value = value;
                OnValueChanged?.Invoke(oldValue, _value);
            } 
        }

        public virtual void SetWithoutNotify(TType newValue)
            => _value = newValue;
        
        [SerializeField, GetSet(nameof(Value))]
        private TType _value;
    }
}
