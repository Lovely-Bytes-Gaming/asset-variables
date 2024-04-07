using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    public sealed class GetSetAttribute : PropertyAttribute
    {
        public readonly string Name;
        public readonly bool ExecuteInEditMode;

        public GetSetAttribute(string name, bool executeInEditMode = false)
        {
            Name = name;
            ExecuteInEditMode = executeInEditMode;
        }
    }
}
