using UnityEngine;
using UnityEditor;

namespace LovelyBytesGaming.AssetVariables
{
    public abstract class ValueTypeEditor<TType> : Editor
        where TType : struct
    {
        protected abstract TType GenericEditorField(string description, TType value);

        public override void OnInspectorGUI()
        {
            var value = (ValueType<TType>)target;
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
