using UnityEngine;

namespace InflamedGums.Util.ScriptableVariables
{
    [AddComponentMenu("Scriptable Variables/Runtime Listener/Vector4 Listener")]
    public class Vector4Listener : ValueTypeListener<Vector4Variable, Vector4> 
    {
#if !UNITY_2020_3_OR_NEWER

        [System.Serializable]
        private class TypedEvent : UnityEngine.Events.UnityEvent<Vector4> {}

        [SerializeField]
        private TypedEvent valueChangedListeners;

        protected override UnityEngine.Events.UnityEvent<Vector4> ValueChangedListeners
            => valueChangedListeners;

#endif
    }
}

