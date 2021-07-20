using UnityEngine;
using UnityEditor;
using System;

namespace CustomLibrary.Util.ScriptableVariables
{
    [CustomEditor(typeof(StringVariable))]
    public class StringVariableEditor : Editor
    {
        private string inputText = "";

        public override void OnInspectorGUI()
        {
            StringVariable var = (StringVariable)target;
            EditorGUILayout.LabelField("Current Value: ", var.Value);
            EditorGUILayout.Space(40f);
            inputText = EditorGUILayout.TextField("new value: ", inputText);
            EditorGUILayout.Space(20f);
            if(GUILayout.Button("Apply Change"))
            {
                var.Value = inputText;
            }

            if (GUI.changed) EditorUtility.SetDirty(var);
        }
    }
}