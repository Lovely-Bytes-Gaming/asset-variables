using UnityEditor;
using UnityEngine;


namespace CustomLibrary.Util.ScriptableVariables
{
    [CustomEditor(typeof(BitMask16Variable))]
    public class BitMask16VariableEditor : Editor
    {
        private enum FlagsEnumerator
        {
            None = 0, 
            bit_0 = 1 << 0, bit_1 = 1 << 1, bit_2 = 1 << 2, bit_3 = 1 << 3,
            bit_4 = 1 << 4, bit_5 = 1 << 5, bit_6 = 1 << 6, bit_7 = 1 << 7,
            bit_8 = 1 << 8, bit_9 = 1 << 9, bit_10 = 1 << 10, bit_11 = 1 << 11,
            bit_12 = 1 << 12, bit_13 = 1 << 13, bit_14 = 1 << 14, bit_15 = 1 << 15,
            All = ~0
        }

        FlagsEnumerator flags;

        public override void OnInspectorGUI()
        {
            BitMask16Variable var = (BitMask16Variable)target;
            EditorGUILayout.LabelField(System.Convert.ToString(var.Value, 2));
            flags = (FlagsEnumerator)var.Value;
            var.Value = (ushort)(FlagsEnumerator)EditorGUILayout.EnumFlagsField(flags);

            if (GUI.changed) EditorUtility.SetDirty(var);
        }
    }
}