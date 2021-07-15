using System.Collections.Generic;
using UnityEngine;

namespace CustomLibs.Util.ScriptableVariables
{
    [CreateAssetMenu(menuName = "Scriptable Objects/BitMasks/32")]
    public class BitMask32Variable : ValueType<uint>
    {
        public void SetBit(int position) => Value |= (uint)(1 << (position & 0x1f));
        public void ClearBit(int position) => Value &= (uint)~(1 << (position & 0x1f));
        public static bool GetBit(uint bitMask, int position) => (bitMask & (1 << (position & 0x1f))) > 0;

        private Dictionary<int, BitMask32Observer> observerPool;
        
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

        public void StopWatchingBit(int position, BitMask32Observer.BitChangedEvent onBitChangedEvent)
        {
            BitMask32Observer obs;
            if (observerPool.TryGetValue(position, out obs))
            {
                obs.OnBitChanged -= onBitChangedEvent;

                if (obs.OnBitChanged == null)
                {
                    observerPool.Remove(obs.BitToWatch);
                    obs = null;
                }
            }
        }
    }
}

