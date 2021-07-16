using System.Collections.Generic;
using UnityEngine;

namespace CustomLibrary.Util.ScriptableVariables
{
    [CreateAssetMenu(menuName = "Scriptable Objects/BitMasks/32")]
    public class BitMask32Variable : ValueType<uint>
    {
        /// <summary>
        /// Sets the bit at 'position' to 1
        /// </summary>
        public void SetBit(int position) => Value |= (uint)(1 << (position & 0x1f));
        /// <summary>
        /// Sets the bit at 'position' to 0
        /// </summary>
        public void ClearBit(int position) => Value &= (uint)~(1 << (position & 0x1f));
        /// <summary>
        /// Returns true if the bit at 'position' of the given 'bitMask' is set to 1, and false otherwise.
        /// </summary>
        public static bool IsBitSet(uint bitMask, int position) => (bitMask & (1 << (position & 0x1f))) > 0;

        // reuse observers based on bit position to ensure that only one observer is created for each bit,
        // regardless of the number of listeners
        private Dictionary<int, BitMask32Observer> observerPool;

        /// <summary>
        /// Convenience method to only get notified when the bit at 'position' changes.
        /// Results in less lines of code, but produces garbage.
        /// Call StopWatchingBit when your gameobject is destroyed or is no longer interested in listening.
        /// </summary>
        public void WatchBit(int position, BitMask32Observer.BitChangedEvent onBitChangedEvent)
        {
            if (observerPool == null)
                observerPool = new Dictionary<int, BitMask32Observer>();

            BitMask32Observer obs;
            if (!observerPool.TryGetValue(position, out obs))
            {
                obs = new BitMask32Observer(this, position);
                observerPool.Add(position, obs);
            }
            obs.OnBitChanged += onBitChangedEvent;
        }

        /// <summary>
        /// Unsubscribes the event and dereferences the underlying observer if no one else is listening to the bit at 'position'.
        /// </summary>
        public void StopWatchingBit(int position, BitMask32Observer.BitChangedEvent onBitChangedEvent)
        {
            BitMask32Observer obs;
            if (observerPool.TryGetValue(position, out obs))
            {
                obs.OnBitChanged -= onBitChangedEvent;

                if (!obs.HasListener)
                {
                    observerPool.Remove(obs.BitToWatch);
                    obs = null;
                }
            }
        }
    }
}

