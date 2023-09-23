using UnityEngine;
using UnityEditor;

namespace LovelyBytesGaming.AssetVariables
{
    public abstract class VariableEditor<TType> : Editor
    {
        protected abstract TType GenericEditorField(string description, TType value);

        public override void OnInspectorGUI()
        {
            var value = (Variable<TType>)target;
            TType newValue = GenericEditorField("Value: ", value.Value);

            if (!GUI.changed)
                return;

            if (Application.isPlaying)
                value.Value = newValue;
            else
                value.SetWithoutNotify(newValue);

            EditorUtility.SetDirty(value);
        }
    }
}
