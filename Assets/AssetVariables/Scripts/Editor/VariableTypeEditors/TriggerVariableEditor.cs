using UnityEngine;
using UnityEditor;

namespace LovelyBytesGaming.AssetVariables
{
    [CustomEditor(typeof(TriggerVariable))]
    public class TriggerVariableEditor : Editor
    {
        private const string _buttonLabel = "Fire";

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if(GUILayout.Button(_buttonLabel) && Application.isPlaying)
            {
                ((TriggerVariable)target).Fire();
            }
        }
    }   
}
