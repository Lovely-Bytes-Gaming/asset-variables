using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Event Types/Trigger")]
public class TriggerVariable : ScriptableObject
{
    public delegate void TriggerFiredEvent();
    public TriggerFiredEvent OnTriggerFired;

    public void Fire() => OnTriggerFired?.Invoke();
}