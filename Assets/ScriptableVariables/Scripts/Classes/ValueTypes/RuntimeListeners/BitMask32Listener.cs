using UnityEngine;
using InflamedGums.Util.Types;

namespace InflamedGums.Util.ScriptableVariables
{
    [AddComponentMenu("Scriptable Variables/Runtime Listener/BitMask32 Listener")]
    public class BitMask32Listener : BitMaskListener<BitMask32Variable, BitMask32> 
    {
#if !UNITY_2020_3_OR_NEWER

        [System.Serializable]
        private class TypedEvent : UnityEngine.Events.UnityEvent<BitMask32Variable.UpdateInfo> {}

        [SerializeField]
        private TypedEvent valueChangedListeners;

        protected override UnityEngine.Events.UnityEvent<BitMask32Variable.UpdateInfo> ValueChangedListeners 
            => valueChangedListeners;

#endif
    }
}

