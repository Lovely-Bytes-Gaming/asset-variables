using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace LovelyBytes.AssetVariables
{
    [CustomPropertyDrawer(typeof(Variable<>), useForChildren: true)]
    public class VariableDrawer : PropertyDrawer
    {
        [SerializeField]
        private VisualTreeAsset _visualTreeAsset;
        
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = _visualTreeAsset.CloneTree();
            var assetPropertyField = root.Q<PropertyField>("Asset");
            assetPropertyField.BindProperty(property);
            UpdateProperty(property, root);
            
            assetPropertyField.TrackPropertyValue(property, 
                changedProperty => UpdateProperty(changedProperty, root));
            
            return root;
        }

        private void UpdateProperty(SerializedProperty property, VisualElement root)
        {
            var valuePropertyField =  root.Q<PropertyField>("Value");
            
            if (PropertyDrawerUtils.TryGetValueProperty(property, out SerializedProperty valueProperty))
            {
                valuePropertyField.style.display = DisplayStyle.Flex;
                valuePropertyField.BindProperty(valueProperty);
            }
            else
            {
                valuePropertyField.style.display = DisplayStyle.None;
                valuePropertyField.Unbind();
            }
        }
    }
}