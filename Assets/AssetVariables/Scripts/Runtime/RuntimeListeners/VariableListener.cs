using UnityEngine;
using UnityEngine.Events;

namespace LovelyBytes.AssetVariables
{
    public abstract class VariableListener<TType> : MonoBehaviour 
    {
        [SerializeField]
        private Variable<TType> _variable;

        [SerializeField]
        private bool _invokeOnAwake;

        [SerializeField]
        private UnityEvent<TType> _valueChangedListeners;
        private void OnValueChanged(TType oldValue, TType newValue)
            => _valueChangedListeners?.Invoke(newValue);

        private void Awake()
        {
            _variable.OnValueChanged += OnValueChanged;

            if (_invokeOnAwake)
                _variable.Value = _variable.Value;
        }

        private void OnDestroy()
            => _variable.OnValueChanged -= OnValueChanged;
    }
}
