using UnityEngine;
using UnityEditor;
using System;

namespace CustomLibrary.Util.ScriptableVariables
{
    [CustomEditor(typeof(InstanceManager))]
    public class InstanceManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var typedTarget = (InstanceManager)target;
            if (typedTarget.referenceDictionary == null) return;

            foreach (var item in typedTarget.referenceDictionary)
            {
                EditorGUILayout.ObjectField($"Item {item.Key}: ", item.Value, typeof(ScriptableObject), true);
            }
        }
    }
}

