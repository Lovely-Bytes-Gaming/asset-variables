using UnityEngine;
using System;

namespace LovelyBytesGaming.AssetVariables
{
    /// <summary>
    /// Base class for enum type variables.
    /// Implementation-scripts of this class can be created via the editor:
    /// Window -> Scriptable Variables -> Create new Enum type.
    /// Instances can be created via the Right Click menu:
    /// Create -> Scriptable Variables -> Enum Types -> Your Type
    /// </summary>
    public abstract class EnumType<TType> : ScriptableObject 
        where TType : Enum
    {
        /// <summary>
        /// Subscribe to this Event to get notified when the value of this object changes.
        /// Provides the new value as parameter.
        /// </summary>
        public event System.Action<TType, TType> OnValueChanged;

        public TType Value
        {
            get => _value;
            set => Utils.SetAndNotify(ref _value, value, OnValueChanged);
        }

        public int ToInt()
            => Convert.ToInt32(Value);

        public void SetWithoutNotify(TType newValue)
            => _value = newValue;
        
        [SerializeField, HideInInspector]
        protected TType _value;
    }
}