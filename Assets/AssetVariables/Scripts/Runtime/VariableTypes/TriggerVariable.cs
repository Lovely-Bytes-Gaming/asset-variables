using System;
using System.Threading;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    [CreateAssetMenu(menuName = AssetVariableConstants.DefaultAssetPath + "Trigger")]
    public class TriggerVariable : ScriptableObject
    {
        public event Action OnTriggerFired;
        
        public void Fire()
        {
            FireSafely();
        }

        private void OnEnable()
        {
            _ = MainThread.ID;
        }

        
        private bool _isFiring;
        private void FireSafely()
        {
            if (Thread.CurrentThread.ManagedThreadId != MainThread.ID)
            {
                Debug.LogError("Trigger can only be fired on the main thread!");
                return;
            }

            if (_isFiring)
            {
                Debug.LogError($"Recursive call to {name}.Fire() will be ignored!");
                return;
            }

            try
            {
                _isFiring = true;
                OnTriggerFired?.Invoke();
            }
            finally
            {
                _isFiring = false;
            }
        }
    }
}

