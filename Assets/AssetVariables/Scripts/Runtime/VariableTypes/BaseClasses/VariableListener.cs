using TNRD;
using UnityEngine;
using UnityEngine.Events;

namespace LovelyBytes.AssetVariables
{
    public abstract class VariableListener<TType> : MonoBehaviour 
    {
        [SerializeField]
        private SerializableInterface<IReadOnlyView<TType>> _variable;

        [SerializeField]
        private bool _invokeOnAwake = false;

        [SerializeField]
        private UnityEvent<TType, TType> _valueChangedListeners;
        private void OnValueChanged(TType oldValue, TType newValue)
            => _valueChangedListeners?.Invoke(oldValue, newValue);

        private void Awake()
        {
            _variable.Value.OnValueChanged += OnValueChanged;

            if (_invokeOnAwake)
                _variable.Value = _variable.Value;
        }

        private void OnDestroy()
            => _variable.Value.OnValueChanged -= OnValueChanged;
    }
}
