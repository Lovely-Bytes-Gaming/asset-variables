using System;
using UnityEditor;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    [CustomEditor(typeof(ScriptableObjectWriter), editorForChildClasses: true)]
    public class ScriptableObjectWriterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            if (GUILayout.Button("Save"))
            {
                (target as ScriptableObjectWriter)?.Save();     
            }
            else if (GUILayout.Button("Load"))
            {
                (target as ScriptableObjectWriter)?.Load();
            }
        }
    }
}
