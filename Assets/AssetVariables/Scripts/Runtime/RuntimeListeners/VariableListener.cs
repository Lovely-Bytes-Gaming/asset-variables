using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace LovelyBytes.AssetVariables
{
    public abstract class VariableListener<TType> : MonoBehaviour 
    {
        [SerializeField]
        private Variable<TType> _variable;

        [SerializeField]
        private bool _invokeOnAwake;

        [FormerlySerializedAs("_valueChangedListeners")] 
        [SerializeField]
        private UnityEvent<TType> _onValueChanged;
        protected virtual void OnValueChanged(TType oldValue, TType newValue)
            => _onValueChanged?.Invoke(newValue);

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
