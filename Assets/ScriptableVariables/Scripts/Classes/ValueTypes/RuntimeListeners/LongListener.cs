using UnityEngine;
using UnityEngine.Events;

namespace InflamedGums.Util.ScriptableVariables
{
    [AddComponentMenu("Scriptable Variables/Runtime Listener/Long Listener")]
    public class LongListener : MonoBehaviour
    {
        [SerializeField]
        private LongVariable longVariable;

        [SerializeField]
        private bool invokeOnAwake = false;

        public UnityEvent<long> ValueChangedListeners;

        private void OnValueChanged(long v)
            => ValueChangedListeners.Invoke(v);

        private void Awake()
        {
            longVariable.OnValueChanged += OnValueChanged;
            
            if (invokeOnAwake)
                longVariable.Invoke();
        }

        private void OnDestroy()
            => longVariable.OnValueChanged -= OnValueChanged;
    }
}

