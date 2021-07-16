using UnityEngine;
using System.Collections.Generic;

namespace CustomLibrary.Util.ScriptableVariables
{
    [CreateAssetMenu(menuName = "Scriptable Objects/BitMasks/16")]
    public class BitMask16Variable : ValueType<ushort>
    {
        public void SetBit(int position) => Value |= (ushort)(1 << (position & 0xf));
        public void ClearBit(int position) => Value &= (ushort)~(1 << (position & 0xf));
        public static bool IsBitSet(ushort bitMask, int position) => (bitMask & (1 << (position & 0xf))) > 0;


        private Dictionary<int, BitMask16Observer> observerPool;
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

        public void StopWatchingBit(int position, BitMask16Observer.BitChangedEvent onBitChangedEvent)
        {
            BitMask16Observer obs;
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