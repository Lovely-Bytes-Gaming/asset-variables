using UnityEngine;
using UnityEditor;

namespace LovelyBytes.AssetVariables
{
    public abstract class VariableEditor<TType> : Editor
    {
        public override void OnInspectorGUI()
        {
            if (!Application.isPlaying)
                base.OnInspectorGUI();
            else
                NotifyWhenChanged();
        }
        
        private void NotifyWhenChanged()
        {
            var variable = target as Variable<TType>;
                
            if (!variable)
                return;
                
            TType oldValue = variable.Value;
            
            // show the base inspector after caching the old variable value for comparison
            base.OnInspectorGUI();

            if (!GUI.changed)
                return;
                
            TType newValue = variable.Value;
                
            // Invokes the "OnValueChanged" event with the correct values for "old" and "new"
            variable.SetWithoutNotify(oldValue);
            variable.Value = newValue;
        }
    }
}
