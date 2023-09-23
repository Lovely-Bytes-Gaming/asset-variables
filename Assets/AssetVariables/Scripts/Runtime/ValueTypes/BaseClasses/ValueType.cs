using UnityEngine;

namespace LovelyBytesGaming.AssetVariables
{
    /// <summary>
    /// Base class for value types (Anything that is a struct)
    /// Instances can be created in the Asset menu via
    /// Create -> Scriptable Objects -> Value Types -> desired type
    /// </summary>
    public abstract class ValueType<TType> : ScriptableObject where TType : struct
    {
        /// <summary>
        /// Subscribe to this Event to get notified when the value of this object changes.
        /// Provides the old and new values as parameters.
        /// </summary>
        public event System.Action<TType, TType> OnValueChanged;

        public virtual TType Value
        {
            get => _value;
            set => Utils.SetAndNotify(ref _value, value, OnValueChanged);
        }

        public virtual void SetWithoutNotify(TType newValue)
            => _value = newValue;
        
        [SerializeField, HideInInspector]
        private TType _value;
    }
}
