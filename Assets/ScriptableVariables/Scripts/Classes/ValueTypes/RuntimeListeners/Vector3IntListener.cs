using UnityEngine;
using UnityEngine.Events;

namespace InflamedGums.Util.ScriptableVariables
{
    [AddComponentMenu("Scriptable Variables/Runtime Listener/Vector3Int Listener")]
    public class Vector3IntListener : MonoBehaviour
    {
        [SerializeField]
        private Vector3IntVariable vector3IntVariable;

        [SerializeField]
        private bool invokeOnAwake = false;

        public UnityEvent<Vector3Int> ValueChangedListeners;

        private void OnValueChanged(Vector3Int v)
            => ValueChangedListeners.Invoke(v);

        private void Awake()
        {
            vector3IntVariable.OnValueChanged += OnValueChanged;
            
            if (invokeOnAwake)
                vector3IntVariable.Invoke();
        }

        private void OnDestroy()
            => vector3IntVariable.OnValueChanged -= OnValueChanged;
    }
}

