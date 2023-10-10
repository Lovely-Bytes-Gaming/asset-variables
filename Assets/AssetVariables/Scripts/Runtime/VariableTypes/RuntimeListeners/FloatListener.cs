using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    [AddComponentMenu(AssetVariableConstants.DefaultAssetPath + "FloatListener")]
    public class FloatListener : VariableListener<FloatVariable, float> 
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

