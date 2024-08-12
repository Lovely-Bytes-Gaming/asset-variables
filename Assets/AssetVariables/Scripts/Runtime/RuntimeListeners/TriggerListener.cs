using UnityEngine;
using UnityEngine.Events;

namespace LovelyBytes.AssetVariables
{
    [AddComponentMenu("LovelyBytes/AssetVariables/TriggerListener")]
    public class TriggerListener : MonoBehaviour
    {
        [SerializeField] 
        private TriggerVariable _trigger;
        
        [SerializeField] 
        private UnityEvent _onTriggerFired;

        private void Awake()
        {
            _trigger.OnTriggerFired += ForwardTrigger;
        }

        private void OnDestroy()
        {
            _trigger.OnTriggerFired -= ForwardTrigger;
        }

        private void ForwardTrigger()
        {
            _onTriggerFired?.Invoke();
        }
    }
}
