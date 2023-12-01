using System;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    [CreateAssetMenu(menuName = AssetVariableConstants.DefaultAssetPath + "Trigger")]
    public class TriggerVariable : ScriptableObject
    {
        public event Action OnTriggerFired;
        
        public void Fire()
        {
            #if ASSET_VARIABLES_SKIP_SAFETY_CHECKS
            OnTriggerFired?.Invoke();
            #else
            _validator.PerformOperation(() => OnTriggerFired?.Invoke());
            #endif
        }

        #if !ASSET_VARIABLES_SKIP_SAFETY_CHECKS
        private OperationValidator _validator;
        
        private void OnEnable()
        {
            _validator = new OperationValidator(name);
        }
        #endif
    }
}

