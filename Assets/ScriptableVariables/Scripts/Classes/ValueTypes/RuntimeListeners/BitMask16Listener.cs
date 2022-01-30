using UnityEngine;
using InflamedGums.Util.Types;

namespace InflamedGums.Util.ScriptableVariables
{
    [AddComponentMenu("Scriptable Variables/Runtime Listener/BitMask16 Listener")]
    public class BitMask16Listener : BitMaskListener<BitMask16Variable, BitMask16> 
    {
#if !UNITY_2020_3_OR_NEWER

        [System.Serializable]
        private class TypedEvent : UnityEngine.Events.UnityEvent<BitMask16Variable.UpdateInfo> {}

        [SerializeField]
        private TypedEvent valueChangedListeners;

        protected override UnityEngine.Events.UnityEvent<BitMask16Variable.UpdateInfo> ValueChangedListeners
            => valueChangedListeners;

#endif
    }
}

