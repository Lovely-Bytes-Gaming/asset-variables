using UnityEngine;
using UnityEngine.Events;
using InflamedGums.Util.Types;

namespace InflamedGums.Util.ScriptableVariables
{
    [AddComponentMenu("Scriptable Variables/Runtime Listener/BitMask16 Listener")]
    public class BitMask16Listener : MonoBehaviour
    {
        [SerializeField]
        private BitMask16Variable bitmask16Variable;

        [SerializeField]
        private bool invokeOnAwake = false;

        public UnityEvent<BitMask16, BitMask16> MaskChangedListeners;

        private void OnValueChanged(BitMask16 newMask, BitMask16 dirty)
            => MaskChangedListeners.Invoke(newMask, dirty);

        private void Awake()
        {
            bitmask16Variable.OnValueChanged += OnValueChanged;

            if (invokeOnAwake)
                bitmask16Variable.SetDirty(new BitMask16(0).Inverse());
        }

        private void OnDestroy()
            => bitmask16Variable.OnValueChanged -= OnValueChanged;
    }
}

