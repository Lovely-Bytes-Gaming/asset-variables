using UnityEngine;
using UnityEngine.Events;

namespace InflamedGums.Util.ScriptableVariables
{
    public abstract class EnumTypeListener<TVar, TType> : MonoBehaviour
        where TVar : EnumType<TType>
        where TType : System.Enum
    {
        [SerializeField]
        private TVar variableAsset;

        [SerializeField]
        private bool invokeOnAwake = false;

#if UNITY_2020_3_OR_NEWER
        [SerializeField]
        private UnityEvent<TType> ValueChangedListeners;
#else
        protected abstract UnityEvent<TType> ValueChangedListeners { get; }
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
