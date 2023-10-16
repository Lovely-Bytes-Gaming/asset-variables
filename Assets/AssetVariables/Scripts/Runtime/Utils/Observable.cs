using System;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    [Serializable]
    public class Observable<TType>
    {
        public TType Value
        {
            get => _value;
#if ASSET_VARIABLES_SKIP_SAFETY_CHECKS
            set => SetValue(value);
#else
            set => Validator.PerformOperation(() => SetValue(value));
#endif
        }

        public event Action<TType, TType> OnValueChanged;

        [SerializeField, GetSet(nameof(Value))] 
        private TType _value;

        #if !ASSET_VARIABLES_SKIP_SAFETY_CHECKS
        private OperationValidator _validator;
        private OperationValidator Validator => _validator ??= 
            new OperationValidator($"Observable<{typeof(TType)}>");
        #endif
        
        public Observable()
        {
            _value = default;
        }
        
        public Observable(TType initialValue)
        {
            _value = initialValue;
        }

        public void SetWithoutNotify(TType value)
        {
            OnBeforeSet(ref value);
            _value = value;
        }

        protected virtual void OnBeforeSet(ref TType value)
        { }
        
        private void SetValue(TType value)
        {
            OnBeforeSet(ref value);
            
            TType oldValue = _value;
            _value = value;
                
            OnValueChanged?.Invoke(oldValue, _value);
        }
    }
}
