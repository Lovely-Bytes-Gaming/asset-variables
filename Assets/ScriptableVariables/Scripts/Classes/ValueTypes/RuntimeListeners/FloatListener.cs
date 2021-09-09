using UnityEngine;
using UnityEngine.Events;

namespace InflamedGums.Util.ScriptableVariables
{
    [AddComponentMenu("Scriptable Variables/Runtime Listener/Float Listener")]
    public class FloatListener : MonoBehaviour
    {
        [SerializeField]
        private FloatVariable floatVariable;

        [SerializeField]
        private bool invokeOnAwake = false;

        public UnityEvent<float> ValueChangedListeners;

        private void OnValueChanged(float v)
            => ValueChangedListeners.Invoke(v);

        private void Awake()
        {
            floatVariable.OnValueChanged += OnValueChanged;
            
            if (invokeOnAwake)
                floatVariable.Invoke();
        }

        private void OnDestroy()
            => floatVariable.OnValueChanged -= OnValueChanged;
    }
}

