using UnityEngine;
using UnityEngine.Events;

namespace InflamedGums.Util.ScriptableVariables
{
    [AddComponentMenu("Scriptable Variables/Runtime Listener/Vector4 Listener")]
    public class Vector4Listener : MonoBehaviour
    {
        [SerializeField]
        private Vector4Variable vector4Variable;

        [SerializeField]
        private bool invokeOnAwake = false;

        public UnityEvent<Vector4> ValueChangedListeners;

        private void OnValueChanged(Vector4 v)
            => ValueChangedListeners.Invoke(v);

        private void Awake()
        {
            vector4Variable.OnValueChanged += OnValueChanged;
            
            if (invokeOnAwake)
                vector4Variable.Invoke();
        }

        private void OnDestroy()
            => vector4Variable.OnValueChanged -= OnValueChanged;
    }
}

