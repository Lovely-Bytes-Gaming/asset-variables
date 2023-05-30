using UnityEngine;

namespace InflamedGums.Util.ScriptableVariables
{
    [AddComponentMenu("Scriptable Variables/Runtime Listener/Vector3Int Listener")]
    public class Vector3IntListener : ValueTypeListener<Vector3IntVariable, Vector3Int> 
    {
#if !UNITY_2020_3_OR_NEWER

        [System.Serializable]
        private class TypedEvent : UnityEngine.Events.UnityEvent<Vector3Int> {}

        [SerializeField]
        private TypedEvent valueChangedListeners;

        protected override UnityEngine.Events.UnityEvent<Vector3Int> ValueChangedListeners
            => valueChangedListeners;

#endif
    }
}

