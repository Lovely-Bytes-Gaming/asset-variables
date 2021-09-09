using UnityEngine;
using UnityEngine.Events;

namespace InflamedGums.Util.ScriptableVariables
{
    [AddComponentMenu("Scriptable Variables/Runtime Listener/Vector3 Listener")]
    public class Vector3Listener : MonoBehaviour
    {
        [SerializeField]
        private Vector3Variable vector3Variable;

        [SerializeField]
        private bool invokeOnAwake = false;

        public UnityEvent<Vector3> ValueChangedListeners;

        private void OnValueChanged(Vector3 v)
            => ValueChangedListeners.Invoke(v);

        private void Awake()
        {
            vector3Variable.OnValueChanged += OnValueChanged;
            
            if (invokeOnAwake)
                vector3Variable.Invoke();
        }

        private void OnDestroy()
            => vector3Variable.OnValueChanged -= OnValueChanged;
    }
}

