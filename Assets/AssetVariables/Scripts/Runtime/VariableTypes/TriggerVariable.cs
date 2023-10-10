using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    [CreateAssetMenu(menuName = AssetVariableConstants.DefaultAssetPath + "Trigger")]
    public class TriggerVariable : ScriptableObject
    {
        public event System.Action OnTriggerFired;

        public void Fire()
        {
            OnTriggerFired?.Invoke();
        }
    }
}

