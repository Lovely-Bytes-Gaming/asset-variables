using UnityEngine;
using UnityEngine.Events;

namespace InflamedGums.Util.ScriptableVariables
{
    [AddComponentMenu("Scriptable Variables/Runtime Listener/Double Listener")]
    public class DoubleListener : MonoBehaviour
    {
        [SerializeField]
        private DoubleVariable doubleVariable;

        [SerializeField]
        private bool invokeOnAwake = false;

        public UnityEvent<double> ValueChangedListeners;

        private void OnValueChanged(double v)
            => ValueChangedListeners.Invoke(v);

        private void Awake()
        {
            doubleVariable.OnValueChanged += OnValueChanged;
            
            if (invokeOnAwake)
                doubleVariable.Invoke();
        }

        private void OnDestroy()
            => doubleVariable.OnValueChanged -= OnValueChanged;
    }
}

