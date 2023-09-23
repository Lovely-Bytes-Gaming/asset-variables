using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LovelyBytesGaming.AssetVariables
{
    internal static class Utils
    {
        internal static void SetAndNotify<TType>(
            ref TType currentValue,
            in TType newValue,
            in System.Action<TType, TType> callback)
        {
            TType oldValue = currentValue;
            currentValue = newValue;
            callback?.Invoke(oldValue, currentValue);
        }
    }
}
