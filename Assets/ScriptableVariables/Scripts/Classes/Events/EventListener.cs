using UnityEngine;
using UnityEngine.Events;


namespace InflamedGums.Util.ScriptableVariables
{
    /// <summary>
    /// Game Objects with this component can listen to ScriptableEvent objects.
    /// </summary>
    public class EventListener : MonoBehaviour
    {
        public ScriptableEvent Event;
        /// <summary>
        /// The Unity Action that is triggered when the event is raised.
        /// </summary>
        public UnityEvent Response;

        private void OnEnable() => Event?.RegisterListener(this); 
        private void OnDisable() => Event?.UnregisterListener(this); 
        public void OnEventRaised() => Response.Invoke();
    }
}