using UnityEngine;
using UnityEditor;

namespace LovelyBytes.AssetVariables
{
    [CustomEditor(typeof(TriggerVariable))]
    public class TriggerEditor : Editor
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
