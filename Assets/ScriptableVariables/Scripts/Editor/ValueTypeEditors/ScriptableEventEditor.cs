using UnityEngine;
using UnityEditor;


namespace CustomLibs.Util.ScriptableVariables
{
    [CustomEditor(typeof(ScriptableEvent))]
    public class ScriptableEventEditor : Editor
    {
        private static string buttonLabel = "Raise";

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if(GUILayout.Button(buttonLabel))
            {
                ((ScriptableEvent)target).Raise();
            }
        }
    }
}
