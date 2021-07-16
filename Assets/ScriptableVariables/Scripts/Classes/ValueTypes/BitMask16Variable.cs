using UnityEngine;
using System.Collections.Generic;

namespace CustomLibrary.Util.ScriptableVariables
{
    [CreateAssetMenu(menuName = "Scriptable Objects/BitMasks/16")]
    public class BitMask16Variable : ValueType<ushort>
    {
        /// <summary>
        /// Sets the bit at 'position' to 1
        /// </summary>
        public void SetBit(int position) => Value |= (ushort)(1 << (position & 0xf));
        /// <summary>
        /// Sets the bit at 'position' to 0
        /// </summary>
        public void ClearBit(int position) => Value &= (ushort)~(1 << (position & 0xf));
        /// <summary>
        /// Returns true if the bit at 'position' of the given 'bitMask' is set to 1, and false otherwise.
        /// </summary>
        public static bool IsBitSet(ushort bitMask, int position) => (bitMask & (1 << (position & 0xf))) > 0;

        // reuse observers based on bit position to ensure that only one observer is created for each bit,
        // regardless of the number of listeners
        private Dictionary<int, BitMask16Observer> observerPool;

        /// <summary>
        /// Convenience method to only get notified when the bit at 'position' changes.
        /// Results in less lines of code, but produces garbage.
        /// Call StopWatchingBit when your gameobject is destroyed or is no longer interested in listening.
        /// </summary>
        public void WatchBit(int position, BitMask16Observer.BitChangedEvent onBitChangedEvent)
        {
            if (observerPool == null) 
                observerPool = new Dictionary<int, BitMask16Observer>();
            
            BitMask16Observer obs;
            if(!observerPool.TryGetValue(position, out obs))
            {
                obs = new BitMask16Observer(this, position);
                observerPool.Add(position, obs);
            }
            obs.OnBitChanged += onBitChangedEvent;
        }

        /// <summary>
        /// Unsubscribes the event and dereferences the underlying observer if no one else is listening to the bit at 'position'.
        /// </summary>
        public void StopWatchingBit(int position, BitMask16Observer.BitChangedEvent onBitChangedEvent)
        {
            BitMask16Observer obs;
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