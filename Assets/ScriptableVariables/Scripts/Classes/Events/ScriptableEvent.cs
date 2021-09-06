using System.Collections.Generic;
using UnityEngine;


namespace InflamedGums.DataManagement.ScriptableVariables
{
    /// <summary>
    /// Event that can be subscribed to by game objects with the EventListener component attached.
    /// </summary>
    [CreateAssetMenu(menuName = "Scriptable Objects/Event Types/Event")]
    public class ScriptableEvent : ScriptableObject
    {
        private List<EventListener> listeners =
            new List<EventListener>();

        /// <summary>
        /// Call 'OnEventRaised' on all Listeners.
        /// </summary>
        public void Raise()
        {
            // Iterate through the the list in reverse order,
            // so the list isn't accessed out of bounds
            // when a listener unregisters itself as a response to the event.
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised();
        }

        public void RegisterListener(EventListener listener) => listeners.Add(listener); 
        public void UnregisterListener(EventListener listener) => listeners.Remove(listener); 
    }
}