using UnityEngine;

namespace InflamedGums.Util.ScriptableVariables
{
    [AddComponentMenu("Scriptable Variables/Runtime Listener/Quaternion Listener")]
    public class QuaternionListener : ValueTypeListener<QuaternionVariable, Quaternion> 
    {
#if !UNITY_2020_3_OR_NEWER

        [System.Serializable]
        private class TypedEvent : UnityEngine.Events.UnityEvent<Quaternion> {}

        [SerializeField]
        private TypedEvent valueChangedListeners;

        protected override UnityEngine.Events.UnityEvent<Quaternion> ValueChangedListeners
            => valueChangedListeners;

#endif
    }
}

