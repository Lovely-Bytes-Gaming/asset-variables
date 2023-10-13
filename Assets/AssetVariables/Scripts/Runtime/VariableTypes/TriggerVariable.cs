using System.Threading;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    [CreateAssetMenu(menuName = AssetVariableConstants.DefaultAssetPath + "Trigger")]
    public class TriggerVariable : ScriptableObject
    {
        public event System.Action OnTriggerFired;

        public void Fire()
        {
            if (Thread.CurrentThread.ManagedThreadId != MainThread.ID)
            {
                Debug.LogError("Trigger can only be fired on the main thread!");
                return;
            }
            OnTriggerFired?.Invoke();
        }

        private void OnEnable()
        {
            _ = MainThread.ID;
        }
    }
}

