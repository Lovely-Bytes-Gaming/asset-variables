using UnityEditor;
using UnityEngine;
using System;

namespace LovelyBytesGaming.AssetVariables
{
    public class EnumTypeEditor<TType> : Editor where TType : Enum
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            var value = (EnumType<TType>)target;
            var newValue = (TType)EditorGUILayout.EnumPopup("Value: ", value.Value);

            if (!GUI.changed)
                return;

            if (Application.isPlaying)
                value.Value = newValue;
            else
                value.SetWithoutNotify(newValue);

            EditorUtility.SetDirty(value);
        }
    }
}

