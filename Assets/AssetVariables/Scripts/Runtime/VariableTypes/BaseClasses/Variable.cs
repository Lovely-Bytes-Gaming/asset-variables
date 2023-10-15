using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    /// <summary>
    /// Base class to create a scriptable object wrapper around the specified generic type.
    /// It provides a Value of the wrapped type, and an OnValueChanged event that
    /// notifies listeners when the value is modified.
    /// </summary>
    public abstract class Variable<TType> : StringSerializable
    {
        /// <summary>
        /// Subscribe to this Event to get notified when the value of this object changes.
        /// Provides the old and new values as parameters.
        /// </summary>
        public event Action<TType, TType> OnValueChanged;

        public TType Value
        {
            get => _value;
#if ASSET_VARIABLES_SKIP_SAFETY_CHECKS
            set => SetValue(value);
#else
            set => _validator.PerformOperation(() => SetValue(value));
#endif
        }

        [SerializeField, GetSet(nameof(Value))]
        private TType _value;
        
        public void SetWithoutNotify(TType newValue)
        {
            OnBeforeSet(ref newValue);
            _value = newValue;
        }

        public override string Serialize(StreamWriter streamWriter)
        {
            MemoryStream memoryStream = new();
            BinaryFormatter binaryFormatter = new();
            binaryFormatter.Serialize(memoryStream, _value);
            
            return Convert.ToBase64String(memoryStream.ToArray());
        }

        public override void Deserialize(in string stringRepresentation)
        {
            byte[] byteRepresentation = Convert.FromBase64String(stringRepresentation);
            
            MemoryStream memoryStream = new (byteRepresentation);
            BinaryFormatter binaryFormatter = new();
            
            _value = (TType)binaryFormatter.Deserialize(memoryStream);
        }
        
        public override string GetKey()
        {
            return name;
        }
        
        /// <summary>
        /// Override this function to perform additional checks and modifications on the value before setting it
        /// </summary>
        /// <param name="newValue">The value that is about to be set.</param>
        protected virtual void OnBeforeSet(ref TType newValue)
        {  }

        private void SetValue(TType value)
        {
            OnBeforeSet(ref value);
            
            TType oldValue = _value;
            _value = value;
                        
            OnValueChanged?.Invoke(oldValue, _value);
        }
        
#if !ASSET_VARIABLES_SKIP_SAFETY_CHECKS
        private OperationValidator _validator;
        
        private void OnEnable()
        {
            _validator = new OperationValidator(name);
        }
#endif
    }
}
