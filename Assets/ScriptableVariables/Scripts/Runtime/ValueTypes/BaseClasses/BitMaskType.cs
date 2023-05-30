using System;
using InflamedGums.Util.Types;

namespace InflamedGums.Util.ScriptableVariables
{
    /// <summary>
    /// TODO: Check if the below usage of generics introduces boxing and refactor if so 
    /// </summary>
    public class BitMaskType<T> : ValueType<T> 
        where T : struct, IBitMask<T>, IEquatable<T>
    {
        public delegate void ValueChangedEvent(in UpdateInfo updateInfo);

        /// <summary>
        /// Subscribe to this Event to get notified when the value of this object changes.
        /// </summary>
        public new event ValueChangedEvent OnValueChanged;

        /// <summary>
        /// Use this when you want to read / change the whole bit mask
        /// </summary>
        public override T Value
        {
            get => base.Value;
            set
            {
                if (!value.Equals(m_Value) && !isLocked)
                {
                    T dirty = m_Value.XOR(value);
                    m_Value = value;
                    SetDirty(dirty);
                }
            }
        }

        /// <summary>
        /// Use this accessor if you want to get/set single values in the mask.
        /// </summary>
        public bool this[int position]
        {
            get => m_Value.Get(position);
            set
            {
                T bm = m_Value;
                bm.Set(position, value);

                if (!bm.Equals(m_Value) && !isLocked)
                {
                    T dirty = bm.XOR(m_Value);
                    m_Value = bm;
                    SetDirty(dirty);
                }
            }
        }

        /// <summary>
        /// Use to broadcast a change in the mask, even though it hasn't actually changed.
        /// Still works when the variable is locked, as the actual value doesn't change.
        /// </summary>
        public void SetDirty(T dirtyMask)
            => OnValueChanged?.Invoke(new UpdateInfo(m_Value, dirtyMask));

        public new void Invoke()
            => SetDirty(new T().Inverse());

        public void Reset() 
            => m_Value.Reset();


        /// <summary>
        /// Data structure that carries information about a bitmask update.
        /// </summary>
        public struct UpdateInfo
        {
            /// <summary>
            /// Bitmask state after the update
            /// </summary>
            public T newMask { get; private set; }
            /// <summary>
            /// Set to true for each bit position that has been changed in the update
            /// </summary>
            public T dirtyBits { get; private set; }

            public UpdateInfo(T newMask, T dirtyBits)
            {
                this.newMask = newMask;
                this.dirtyBits = dirtyBits;
            }
        }
    }
}