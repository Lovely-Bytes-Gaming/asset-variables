using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace InflamedGums.Util.ScriptableVariables
{
    public class ValueTypeListener<TVar, TType> : MonoBehaviour 
        where TVar : ValueType<TType> 
        where TType : struct, IEquatable<TType>
    {
        [SerializeField]
        private TVar variableAsset;

        [SerializeField]
        private bool invokeOnAwake = false;

#if UNITY_2020_3_OR_NEWER
        [SerializeField]
        private UnityEvent<TType> ValueChangedListeners;
#else
        [Serializable]
        private class TypedUnityEvent : UnityEvent<TType> { }
        [SerializeField]
        private TypedUnityEvent ValueChangedListeners;
#endif
        private void OnValueChanged(TType v)
            => ValueChangedListeners?.Invoke(v);

        private void Awake()
        {
            variableAsset.OnValueChanged += OnValueChanged;

            if (invokeOnAwake)
                variableAsset.Invoke();
        }

        private void OnDestroy()
            => variableAsset.OnValueChanged -= OnValueChanged;
    }
}
