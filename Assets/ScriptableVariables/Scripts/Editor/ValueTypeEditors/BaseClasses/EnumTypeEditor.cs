using UnityEditor;
using UnityEngine;
using System;

namespace CustomLibrary.Util.ScriptableVariables
{
    public class EnumTypeEditor<T> : Editor where T : Enum
    {
        public override void OnInspectorGUI()
        {
            EnumType<T> var = (EnumType<T>)target;
            var.Value = (T)EditorGUILayout.EnumPopup("Value: ", var.Value);

            var.isLocked = EditorGUILayout.Toggle("Locked: ", var.isLocked);


            if (GUI.changed) EditorUtility.SetDirty(var);
        }
    }
}

