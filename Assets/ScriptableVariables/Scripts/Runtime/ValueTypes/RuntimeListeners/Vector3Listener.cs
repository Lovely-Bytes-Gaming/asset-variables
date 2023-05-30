using UnityEngine;

namespace InflamedGums.Util.ScriptableVariables
{
    [AddComponentMenu("Scriptable Variables/Runtime Listener/Vector3 Listener")]
    public class Vector3Listener : ValueTypeListener<Vector3Variable, Vector3> 
    {
#if !UNITY_2020_3_OR_NEWER

        [System.Serializable]
        private class TypedEvent : UnityEngine.Events.UnityEvent<Vector3> {}

        [SerializeField]
        private TypedEvent valueChangedListeners;

        protected override UnityEngine.Events.UnityEvent<Vector3> ValueChangedListeners
            => valueChangedListeners;

#endif
    }
}

