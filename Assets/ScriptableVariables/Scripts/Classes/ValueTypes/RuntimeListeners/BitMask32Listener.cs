using UnityEngine;
using InflamedGums.Util.Types;
using UnityEngine.Events;

namespace InflamedGums.Util.ScriptableVariables
{
    [AddComponentMenu("Scriptable Variables/Runtime Listener/BitMask32 Listener")]
    public class BitMask32Listener : MonoBehaviour
    {
        [SerializeField]
        private BitMask32Variable bitmask32Variable;

        [SerializeField]
        private bool invokeOnAwake = false;

        public UnityEvent<BitMask32, BitMask32> MaskChangedListeners;

        private void OnValueChanged(BitMask32 newMask, BitMask32 dirty)
            => MaskChangedListeners.Invoke(newMask, dirty);

        private void Awake()
        {
            bitmask32Variable.OnValueChanged += OnValueChanged;

            if (invokeOnAwake)
                bitmask32Variable.SetDirty(new BitMask32(0).Inverse());
        }

        private void OnDestroy()
            => bitmask32Variable.OnValueChanged -= OnValueChanged;
    }
}

