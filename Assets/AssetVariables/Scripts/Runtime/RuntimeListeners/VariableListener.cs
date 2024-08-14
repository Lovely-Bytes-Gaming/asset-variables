using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace LovelyBytes.AssetVariables
{
    public abstract class VariableListener<TType> : MonoBehaviour 
    {
        [SerializeField]
        private Variable<TType> _variable;

        [FormerlySerializedAs("_invokeOnAwake")] [SerializeField]
        private bool _invokeOnStart = true;

        [FormerlySerializedAs("_valueChangedListeners")] 
        [SerializeField]
        private UnityEvent<TType> _onValueChanged;
        protected virtual void OnValueChanged(TType oldValue, TType newValue)
            => _onValueChanged?.Invoke(newValue);

        private void Awake()
        {
            _variable.OnValueChanged += OnValueChanged;
        }

        private void Start()
        {
            if (_invokeOnStart)
                OnValueChanged(default, _variable.Value);
        }

        private void OnDestroy()
            => _variable.OnValueChanged -= OnValueChanged;
    }
}
