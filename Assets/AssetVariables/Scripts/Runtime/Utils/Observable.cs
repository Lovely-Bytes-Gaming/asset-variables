using System;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    [Serializable]
    public class Observable<TValue>
    {
        public TValue Value
        {
            get => _value;
#if ASSET_VARIABLES_SKIP_SAFETY_CHECKS
            set => SetValue(value);
#else
            set => _validator.PerformOperation(() => SetValue(value));
#endif
        }

        public event Action<TValue, TValue> OnValueChanged;

        [SerializeField, GetSet(nameof(Value))] 
        private TValue _value;

        #if !ASSET_VARIABLES_SKIP_SAFETY_CHECKS
        private OperationValidator _validator;
        #endif
        
        public Observable(TValue initialValue)
        {
            _value = initialValue;
            
            #if !ASSET_VARIABLES_SKIP_SAFETY_CHECKS
            _validator = new OperationValidator($"Observable<{typeof(TValue)}>");
            #endif
        }

        private void SetValue(TValue value)
        {
            TValue oldValue = _value;
            _value = value;
                
            OnValueChanged?.Invoke(oldValue, _value);
        }
    }
}
