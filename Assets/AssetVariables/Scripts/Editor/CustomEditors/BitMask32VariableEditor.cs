using System.Linq;
using UnityEditor;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    [CustomEditor(typeof(BitMask32Variable))]
    public class BitMask32VariableEditor : Editor
    {
        private static readonly string[] _labels = 
            Enumerable
                .Range(0, 32)
                .Select(x => x.ToString())
                .ToArray();
        
        public override void OnInspectorGUI()
        {
            if (target is not BitMask32Variable bitMask32Variable)
                return;
            
            BitMask32 newValue = EditorGUILayout.MaskField(bitMask32Variable.Value, 
                _labels);

            if (Application.isPlaying)
                bitMask32Variable.Value = newValue;
            else
                bitMask32Variable.SetWithoutNotify(newValue);
            
            if(GUI.changed)
                EditorUtility.SetDirty(target);
        }
    }
}