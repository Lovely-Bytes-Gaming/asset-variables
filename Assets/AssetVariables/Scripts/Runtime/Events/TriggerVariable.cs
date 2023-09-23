using UnityEngine;

namespace LovelyBytesGaming.AssetVariables
{
    [CreateAssetMenu(menuName = "LovelyBytesGaming/AssetVariables/Trigger")]
    public class TriggerVariable : ScriptableObject
    {
        public event System.Action OnTriggerFired;

        public void Fire()
        {
            OnTriggerFired?.Invoke();
        }
    }
}

