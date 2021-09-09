using UnityEngine;
using UnityEngine.Events;

namespace InflamedGums.Util.ScriptableVariables
{
    [AddComponentMenu("Scriptable Variables/Runtime Listener/Vector2 Listener")]
    public class Vector2Listener : MonoBehaviour
    {
        [SerializeField]
        private Vector2Variable vector2Variable;

        [SerializeField]
        private bool invokeOnAwake = false;

        public UnityEvent<Vector2> ValueChangedListeners;

        private void OnValueChanged(Vector2 v)
            => ValueChangedListeners.Invoke(v);

        private void Awake()
        {
            vector2Variable.OnValueChanged += OnValueChanged;
            
            if (invokeOnAwake)
                vector2Variable.Invoke();
        }

        private void OnDestroy()
            => vector2Variable.OnValueChanged -= OnValueChanged;
    }
}

