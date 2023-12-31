using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    public sealed class GetSetAttribute : PropertyAttribute
    {
        public readonly string Name;

        public GetSetAttribute(string name)
        {
            Name = name;
        }
    }
}
