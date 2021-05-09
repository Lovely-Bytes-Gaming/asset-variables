using UnityEngine;
using UnityEditor;


namespace CustomLibs.Util.ScriptableVariables
{
    [CustomEditor(typeof(ScriptableEvent))]
    public class ScriptableEventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if(GUILayout.Button("Raise"))
            {
                ((ScriptableEvent)target).Raise();
            }
        }
    }
}
