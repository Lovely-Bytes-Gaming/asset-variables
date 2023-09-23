using UnityEngine;
using UnityEngine.Events;

namespace LovelyBytesGaming.AssetVariables
{
    public abstract class ValueTypeListener<TVar, TType> : MonoBehaviour 
        where TVar : ValueType<TType> 
        where TType : struct
    {
        [SerializeField]
        private TVar _variableAsset;

        [SerializeField]
        private bool _invokeOnAwake = false;

        [SerializeField]
        private UnityEvent<TType, TType> _valueChangedListeners;
        private void OnValueChanged(TType oldValue, TType newValue)
            => _valueChangedListeners?.Invoke(oldValue, newValue);

        private void Awake()
        {
            _variableAsset.OnValueChanged += OnValueChanged;

            if (_invokeOnAwake)
                _variableAsset.Value = _variableAsset.Value;
        }

        private void OnDestroy()
            => _variableAsset.OnValueChanged -= OnValueChanged;
    }
}
