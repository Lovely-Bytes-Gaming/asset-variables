using UnityEngine;

namespace InflamedGums.Util.ScriptableVariables
{
    [AddComponentMenu("Scriptable Variables/Runtime Listener/Vector2Int Listener")]
    public class Vector2IntListener : ValueTypeListener<Vector2IntVariable, Vector2Int> 
    {
#if !UNITY_2020_3_OR_NEWER

        [System.Serializable]
        private class TypedEvent : UnityEngine.Events.UnityEvent<Vector2Int> {}

        [SerializeField]
        private TypedEvent valueChangedListeners;

        protected override UnityEngine.Events.UnityEvent<Vector2Int> ValueChangedListeners
            => valueChangedListeners;

#endif
    }
}

