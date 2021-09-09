using UnityEngine;
using UnityEngine.Events;

namespace InflamedGums.Util.ScriptableVariables
{
    [AddComponentMenu("Scriptable Variables/Runtime Listener/Int Listener")]
    public class IntListener : MonoBehaviour
    {
        [SerializeField]
        private IntVariable intVariable;

        [SerializeField]
        private bool invokeOnAwake = false;

        public UnityEvent<int> ValueChangedListeners;

        private void OnValueChanged(int v)
            => ValueChangedListeners.Invoke(v);

        private void Awake()
        {
            intVariable.OnValueChanged += OnValueChanged;
            
            if (invokeOnAwake)
                intVariable.Invoke();
        }

        private void OnDestroy()
            => intVariable.OnValueChanged -= OnValueChanged;
    }
}

