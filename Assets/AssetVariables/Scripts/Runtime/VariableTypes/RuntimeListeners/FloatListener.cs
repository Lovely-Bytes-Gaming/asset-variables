using UnityEngine;

namespace LovelyBytesGaming.AssetVariables
{
    [AddComponentMenu("LovelyBytesGaming/AssetVariables/FloatListener")]
    public class FloatListener : VariableTypeListener<FloatVariable, float> 
    {
#if !UNITY_2020_3_OR_NEWER

        [System.Serializable]
        private class TypedEvent : UnityEngine.Events.UnityEvent<float> {}

        [SerializeField]
        private TypedEvent valueChangedListeners;

        protected override UnityEngine.Events.UnityEvent<float> ValueChangedListeners 
            => valueChangedListeners;

#endif
    }
}

