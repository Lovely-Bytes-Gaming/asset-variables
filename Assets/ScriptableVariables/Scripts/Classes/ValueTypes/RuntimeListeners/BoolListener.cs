using UnityEngine;
using UnityEngine.Events;

namespace InflamedGums.Util.ScriptableVariables
{
    [AddComponentMenu("Scriptable Variables/Runtime Listener/Bool Listener")]
    public class BoolListener : MonoBehaviour
    {
        [SerializeField]
        private BoolVariable boolVariable;

        [SerializeField]
        private bool invokeOnAwake = false;

        public UnityEvent<bool> ValueChangedListeners;

        private void OnValueChanged(bool v)
            => ValueChangedListeners.Invoke(v);

        private void Awake()
        {
            boolVariable.OnValueChanged += OnValueChanged;
            
            if (invokeOnAwake)
                boolVariable.Invoke();
        }

        private void OnDestroy()
            => boolVariable.OnValueChanged -= OnValueChanged;
    }
}

