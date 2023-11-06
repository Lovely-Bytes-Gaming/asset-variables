using UnityEditor;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    public class RuntimeListEditor<TType> : Editor
        where TType : Object
    {
        private bool _isExpanded = false;
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            if (target is not ListObject<TType> list)
                return;
            
            _isExpanded = EditorGUILayout.Foldout(_isExpanded, "");

            if (!_isExpanded) 
                return;
            
            foreach (TType item in list)
                EditorGUILayout.ObjectField(item, typeof(TType), true);
        }
    }
}
