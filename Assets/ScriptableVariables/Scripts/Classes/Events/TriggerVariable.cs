using UnityEngine;

/// <summary>
/// Simple trigger. lightweight, but less functionality than scriptable events.
/// </summary>
[CreateAssetMenu(menuName = "Scriptable Objects/Event Types/Trigger")]
public class TriggerVariable : ScriptableObject
{
    public delegate void TriggerFiredEvent();
    public event TriggerFiredEvent OnTriggerFired;

    public void Fire() => OnTriggerFired?.Invoke();
}