using UnityEngine;
using System;

/// <summary>
/// Simple trigger. lightweight, but less functionality than scriptable events.
/// </summary>
[CreateAssetMenu(menuName = "Scriptable Objects/Event Types/Trigger")]
public class TriggerVariable : ScriptableObject
{
    public delegate void TriggerFiredEvent();
    public event TriggerFiredEvent OnTriggerFired;

    public bool isLocked =  false;

    public void Fire()
    {
        if (!isLocked) 
            OnTriggerFired?.Invoke();
    }
}