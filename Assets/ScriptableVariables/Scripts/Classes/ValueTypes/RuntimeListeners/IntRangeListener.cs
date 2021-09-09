using UnityEngine;
using UnityEngine.Events;

namespace InflamedGums.Util.ScriptableVariables
{
    [AddComponentMenu("Scriptable Variables/Runtime Listener/Int Range Listener")]
    public class IntRangeListener : MonoBehaviour
    {
        [SerializeField]
        private IntRange intRangeVariable;

        [SerializeField]
        private bool invokeOnAwake = false;

        public UnityEvent<int> ValueChangedListeners;

        private void OnValueChanged(int v)
            => ValueChangedListeners.Invoke(v);

        private void Awake()
        {
            intRangeVariable.OnValueChanged += OnValueChanged;
            
            if (invokeOnAwake)
                intRangeVariable.Invoke();
        }

        private void OnDestroy()
            => intRangeVariable.OnValueChanged -= OnValueChanged;
    }
}

