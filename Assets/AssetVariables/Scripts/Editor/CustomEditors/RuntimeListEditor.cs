using System.Linq;
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
            
            if (target is not RuntimeList<TType> list)
                return;

            if (!Application.isPlaying && list.Count > 0)
            {
                list.Clear();
                return;
            }
            
            _isExpanded = EditorGUILayout.Foldout(_isExpanded, "");

            if (!_isExpanded) 
                return;
            
            foreach (TType item in list)
                EditorGUILayout.ObjectField(item, typeof(TType), true);
        }
    }
}
