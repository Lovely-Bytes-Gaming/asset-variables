using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ScriptableObjectSet))]
public class ScriptableObjectSetEditor : Editor
{ 
    private ScriptableObjectSet m_typedTarget;
    private List<ScriptableObject> m_entries;
    private bool m_foldout;

    public void OnEnable()
    {
        m_typedTarget = target as ScriptableObjectSet;
        m_entries = new List<ScriptableObject>();

        m_entries.AddRange(m_typedTarget.GetAll());
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.Space();
        DrawDragAndDrop();
        EditorGUILayout.Space();
        DrawContentArray();
    }

    private void DrawDragAndDrop()
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
                            m_typedTarget.Add(dragged_object as ScriptableObject);
                            modified = true;
                        }
                    }
                    if (modified)
                    {
                        EditorUtility.SetDirty(m_typedTarget);
                    }
                }
                break;
        }
    }

    private void DrawContentArray()
    {
        m_entries.Clear();
        m_entries.AddRange(m_typedTarget.GetAll());

        m_foldout = EditorGUILayout.Foldout(m_foldout, "Content");

        if (m_foldout)
        {
            for (int i = 0; i < m_entries.Count; i++)
            {
                var newValue = EditorGUILayout.ObjectField(m_entries[i], typeof(ScriptableObject), false);
                if (!newValue)
                {
                    var keyStr = m_entries[i].name;
                    m_typedTarget.Remove(keyStr);
                }
                else if (newValue != m_entries[i])
                {
                    var keyStr = m_entries[i].name;
                    m_typedTarget.Change(keyStr, newValue as ScriptableObject);
                }
            }
        }
    }
}
