using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ScriptableObjectSet))]
public class ScriptableObjectSetEditor : Editor
{ 
    private ScriptableObjectSet typedTarget;

    public void OnEnable()
    {
        typedTarget = target as ScriptableObjectSet;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.Space();
        DropAreaGUI();
    }

    public void DropAreaGUI()
    {
        Event evt = Event.current;
        Rect drop_area = GUILayoutUtility.GetRect(0.0f, 25.0f, GUILayout.ExpandWidth(true));
        GUI.Box(drop_area, "DRAG OBJECTS HERE");

        switch (evt.type)
        {
            case EventType.DragUpdated:
            case EventType.DragPerform:
                if (!drop_area.Contains(evt.mousePosition))
                    return;

                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                if (evt.type == EventType.DragPerform)
                {
                    DragAndDrop.AcceptDrag();
                    
                    bool modified = false;
                    
                    foreach (Object dragged_object in DragAndDrop.objectReferences)
                    {
                        if(dragged_object.GetType().IsSubclassOf(typeof(ScriptableObject)))
                        {
                            typedTarget.Add(dragged_object as ScriptableObject);
                            modified = true;
                        }
                    }
                    if (modified)
                    {
                        EditorUtility.SetDirty(typedTarget);
                    }
                }
                break;
        }

        EditorGUILayout.LabelField("Registered Objects", EditorStyles.boldLabel);

        EditorGUI.BeginDisabledGroup(true);
        foreach(var obj in typedTarget.GetAll())
        {
            EditorGUILayout.ObjectField(obj, typeof(ScriptableObject), false);
        }
        EditorGUI.EndDisabledGroup();
    }
}
