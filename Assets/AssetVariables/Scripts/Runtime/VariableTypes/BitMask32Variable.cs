using System;
using UnityEngine;

namespace LovelyBytesGaming.AssetVariables
{
    [CreateAssetMenu(menuName = "LovelyBytesGaming/AssetVariables/BitMask32")]
    public class BitMask32Variable : Variable<BitMask32>
    {
        /// <summary>
        /// Use this accessor if you want to get/set single values in the mask.
        /// </summary>
        public bool this[int position]
        {
            get => Value[position];
            set
            {
                BitMask32 currentMask = Value;
                currentMask[position] = value;
                Value = currentMask;
            }
        }
    }
}

