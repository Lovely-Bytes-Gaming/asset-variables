using UnityEngine;
using UnityEngine.Events;

namespace InflamedGums.Util.ScriptableVariables
{
    [AddComponentMenu("Scriptable Variables/Runtime Listener/Vector2Int Listener")]
    public class Vector2IntListener : MonoBehaviour
    {
        [SerializeField]
        private Vector2IntVariable vector2IntVariable;

        [SerializeField]
        private bool invokeOnAwake = false;

        public UnityEvent<Vector2Int> ValueChangedListeners;

        private void OnValueChanged(Vector2Int v)
            => ValueChangedListeners.Invoke(v);

        private void Awake()
        {
            vector2IntVariable.OnValueChanged += OnValueChanged;
            
            if (invokeOnAwake)
                vector2IntVariable.Invoke();
        }

        private void OnDestroy()
            => vector2IntVariable.OnValueChanged -= OnValueChanged;
    }
}

