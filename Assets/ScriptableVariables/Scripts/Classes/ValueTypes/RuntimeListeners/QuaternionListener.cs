using UnityEngine;
using UnityEngine.Events;

namespace InflamedGums.Util.ScriptableVariables
{
    [AddComponentMenu("Scriptable Variables/Runtime Listener/Quaternion Listener")]
    public class QuaternionListener : MonoBehaviour
    {
        [SerializeField]
        private QuaternionVariable quaternionVariable;

        [SerializeField]
        private bool invokeOnAwake = false;

        public UnityEvent<Quaternion> ValueChangedListeners;

        private void OnValueChanged(Quaternion v)
            => ValueChangedListeners.Invoke(v);

        private void Awake()
        {
            quaternionVariable.OnValueChanged += OnValueChanged;
            
            if (invokeOnAwake)
                quaternionVariable.Invoke();
        }

        private void OnDestroy()
            => quaternionVariable.OnValueChanged -= OnValueChanged;
    }
}

