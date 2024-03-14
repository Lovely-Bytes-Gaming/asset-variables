using UnityEngine;
using UnityEngine.Events;

namespace LovelyBytes.AssetVariables
{
    [AddComponentMenu(AssetVariableConstants.DefaultAssetPath + "BoolListener")]
    public class BoolListener : VariableListener<bool>
    {
        [SerializeField] private UnityEvent _onTrue, _onFalse;
        
        protected override void OnValueChanged(bool oldValue, bool newValue)
        {
            base.OnValueChanged(oldValue, newValue);

            if (newValue)
                _onTrue?.Invoke();
            else
                _onFalse?.Invoke();
        }
    }
}

