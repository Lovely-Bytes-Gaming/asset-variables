using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TriggerVariable))]
public class TriggerVariableEditor : Editor
{
    private static string buttonLabel = "Fire";

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button(buttonLabel))
        {
            ((TriggerVariable)target).Fire();
        }
    }
}
