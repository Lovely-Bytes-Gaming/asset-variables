using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    /// <summary>
    /// Base class to create a scriptable object wrapper around the specified generic type.
    /// It provides a Value of the wrapped type, and an OnValueChanged event that
    /// notifies listeners when the value is modified.
    /// </summary>
    public abstract class Variable<TType> : ScriptableObject
    {
        /// <summary>
        /// Subscribe to this Event to get notified when the value of this object changes.
        /// Provides the old and new values as parameters.
        /// </summary>
        public event Action<TType, TType> OnValueChanged;

        public TType Value
        {
            get => _value;
            set
            {
                if (Thread.CurrentThread.ManagedThreadId != MainThread.ID)
                {
                    Debug.LogError($"{GetType().Name}.Value can only be set on the main thread!");
                    return;
                }
                
                if (_isRecursiveCallback)
                {
                    if (_recursionDepth < AssetVariableConstants.MaxSetValueRecursionDepth)
                    {
                        ++_recursionDepth;
                        _pendingSetOperations.Enqueue(value);
                    }
                    else
                    {
                        Debug.LogError($"Exceeded the recursion limit of {AssetVariableConstants.MaxSetValueRecursionDepth} " +
                                       $"while setting the 'Value' property of {name}. Maybe you are re-assigning the value from within a method that listens to the OnValueChanged callback?");
                    }
                    return;
                }
                try
                {
                    OnBeforeSet(ref value);
                    
                    TType oldValue = _value;
                    _value = value;
                    
                    _isRecursiveCallback = true;
                    OnValueChanged?.Invoke(oldValue, _value);
                }
                catch (Exception)
                {
                    _recursionDepth = 0;
                    _pendingSetOperations.Clear();
                    throw;
                }
                finally
                {
                    _isRecursiveCallback = false;
                }

                if (_pendingSetOperations.Count > 0)
                {
                    Value = _pendingSetOperations.Dequeue();
                    return;
                }
                _recursionDepth = 0;
            }
        }

        [SerializeField, GetSet(nameof(Value))]
        private TType _value;
        
        private bool _isRecursiveCallback = false;
        private int _recursionDepth = 0;
        private readonly Queue<TType> _pendingSetOperations = new();
        
        public void SetWithoutNotify(TType newValue)
        {
            OnBeforeSet(ref newValue);
            _value = newValue;
        }
        
        /// <summary>
        /// Override this function to perform additional checks and modifications on the value before setting it
        /// </summary>
        /// <param name="newValue">The value that is about to be set.</param>
        protected virtual void OnBeforeSet(ref TType newValue)
        {  }

        private void OnEnable()
        {
            // Reference the MainThread static class from the main thread to ensure it is initialized
            // with the correct ID
            _ = MainThread.ID;
        }
    }
}
