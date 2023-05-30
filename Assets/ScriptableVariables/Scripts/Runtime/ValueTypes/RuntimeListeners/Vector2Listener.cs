using UnityEngine;

namespace InflamedGums.Util.ScriptableVariables
{
    [AddComponentMenu("Scriptable Variables/Runtime Listener/Vector2 Listener")]
    public class Vector2Listener : ValueTypeListener<Vector2Variable, Vector2> 
    {
#if !UNITY_2020_3_OR_NEWER

        [System.Serializable]
        private class TypedEvent : UnityEngine.Events.UnityEvent<Vector2> {}

        [SerializeField]
        private TypedEvent valueChangedListeners;

        protected override UnityEngine.Events.UnityEvent<Vector2> ValueChangedListeners
            => valueChangedListeners;

#endif
    }
}

