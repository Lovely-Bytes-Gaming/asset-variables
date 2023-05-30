using UnityEngine;

namespace InflamedGums.Util.ScriptableVariables
{
    [AddComponentMenu("Scriptable Variables/Runtime Listener/Bool Listener")]
    public class BoolListener : ValueTypeListener<BoolVariable, bool> 
    {
#if !UNITY_2020_3_OR_NEWER

        [System.Serializable]
        private class TypedEvent : UnityEngine.Events.UnityEvent<bool> {}

        [SerializeField]
        private TypedEvent valueChangedListeners;

        protected override UnityEngine.Events.UnityEvent<bool> ValueChangedListeners
            => valueChangedListeners;

#endif
    }
}

