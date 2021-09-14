using System;
using UnityEngine;
using UnityEngine.Events;
using InflamedGums.Util.Types;

namespace InflamedGums.Util.ScriptableVariables
{
    public class BitMaskListener<TVar, TType> : MonoBehaviour 
        where TVar : BitMaskType<TType> 
        where TType : struct, IBitMask<TType>, IEquatable<TType>
    {
        [SerializeField]
        private TVar bitMaskVariable;

        [SerializeField]
        private bool invokeOnAwake = false;

#if UNITY_2020_3_OR_NEWER
        [SerializeField]
        private UnityEvent<TType, TType> ValueChangedListeners;
#else
        [Serializable]
        private class TypedUnityEvent : UnityEvent<TType, TType> { }
        [SerializeField]
        private TypedUnityEvent ValueChangedListeners;
#endif

        private void OnValueChanged(TType newMask, TType dirty)
            => ValueChangedListeners.Invoke(newMask, dirty);

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

