using System;
using UnityEngine;
using UnityEngine.Events;
using InflamedGums.Util.Types;

namespace InflamedGums.Util.ScriptableVariables
{
    public abstract class BitMaskListener<TVar, TType> : MonoBehaviour 
        where TVar : BitMaskType<TType> 
        where TType : struct, IBitMask<TType>, IEquatable<TType>
    {
        [SerializeField]
        private TVar bitMaskVariable;

        [SerializeField]
        private bool invokeOnAwake = false;

#if UNITY_2020_3_OR_NEWER
        [SerializeField]
        private UnityEvent<BitMaskType<TType>.UpdateInfo> ValueChangedListeners;
#else
        protected abstract UnityEvent<BitMaskType<TType>.UpdateInfo> ValueChangedListeners { get; }
#endif

        private void OnValueChanged(BitMaskType<TType>.UpdateInfo updateInfo)
            => ValueChangedListeners?.Invoke(updateInfo);

        private void Awake()
        {
            bitMaskVariable.OnValueChanged += OnValueChanged;

            if (invokeOnAwake)
                bitMaskVariable.Invoke();
        }

        private void OnDestroy()
            => bitMaskVariable.OnValueChanged -= OnValueChanged;
    }
}

