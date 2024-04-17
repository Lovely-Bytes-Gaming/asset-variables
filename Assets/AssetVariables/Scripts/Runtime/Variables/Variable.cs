using System;
using System.Diagnostics;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    /// <summary>
    /// Base class to create a scriptable object wrapper around the specified generic type.
    /// It provides a Value of the wrapped type, and an OnValueChanged event that
    /// notifies listeners when the value is modified.
    /// </summary>
    public abstract class Variable<TType> : ScriptableObject, 
        IReadWriteView<TType>, IReadOnlyView<TType>
    {
        /// <summary>
        /// Subscribe to this Event to get notified when the Value property is set.
        /// Provides the old and new values as parameters.
        /// </summary>
        public event Action<TType, TType> OnValueChanged;

        public TType Value
        {
            get => _value;
#if ASSET_VARIABLES_SKIP_SAFETY_CHECKS
            set => SetValue(value);
#else
            set => Validator.PerformOperation(() => SetValue(value));
#endif
        }

        [SerializeField, GetSet(name: nameof(Value), executeInEditMode: true)] 
        private TType _value;

        [SerializeField] 
        private DefaultValue<TType> _defaultValue;
        
        /// <summary>
        /// Modifies the value without invoking any events
        /// </summary>
        public void SetWithoutNotify(TType newValue)
        {
            OnBeforeSet(ref newValue);
            _value = newValue;
        }

        /// <summary>
        /// Override this function to perform additional checks and modifications on the value before it is set
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

        private void Awake()
        {
            if (_defaultValue.Use)
                _value = _defaultValue.Value;

            ListenToPlaymodeChanges();            
            AwakeOverride();
        }

        private void OnEnable()
        {
            ListenToPlaymodeChanges();
            OnEnableOverride();
        }

        /// <summary>
        /// Override this method instead of defining your own Awake method to avoid shadowing in child classes
        /// </summary>
        protected virtual void AwakeOverride() {}
        
        /// <summary>
        /// Override this method instead of defining your own OnEnable method to avoid shadowing in child classes
        /// </summary>
        protected virtual void OnEnableOverride() {}

#if !ASSET_VARIABLES_SKIP_SAFETY_CHECKS
        private OperationValidator _validator;
        private OperationValidator Validator => _validator ??= 
            new OperationValidator(name);
#endif

        
        [Conditional("UNITY_EDITOR")]
        private void ListenToPlaymodeChanges()
        {
            UnityEditor.EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

#if UNITY_EDITOR
        private TType _editModeValue;
        private void OnPlayModeStateChanged(UnityEditor.PlayModeStateChange state)
        {
            switch (state)
            {
                case UnityEditor.PlayModeStateChange.EnteredPlayMode:
                    _editModeValue = _value;
                    break;
                case UnityEditor.PlayModeStateChange.ExitingPlayMode:
                    _value = _editModeValue;
                    break;
            }
        }
#endif
    }
}
