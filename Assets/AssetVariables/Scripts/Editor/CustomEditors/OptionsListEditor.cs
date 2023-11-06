using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    public class OptionsListEditor<TType> : RuntimeListEditor<TType>
        where TType : Object
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            if (!Application.isPlaying || target is not OptionsList<TType> list)
                return;
            
            if (GUILayout.Button("Move To Next"))
                list.MoveToNext();
            
            if (GUILayout.Button("Move To Previous"))
                list.MoveToPrevious();
        }
    }
}
