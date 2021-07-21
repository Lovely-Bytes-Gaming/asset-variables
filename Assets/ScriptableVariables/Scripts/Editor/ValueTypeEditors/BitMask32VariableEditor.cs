using UnityEditor;
using UnityEngine;


namespace CustomLibrary.Util.ScriptableVariables
{
    [CustomEditor(typeof(BitMask32Variable))]
    public class BitMask32VariableEditor : Editor
    {
        private enum FlagsEnumerator
        {
            None = 0, 

            bit_0 = 1 << 0, bit_1 = 1 << 1, bit_2 = 1 << 2, bit_3 = 1 << 3,
            bit_4 = 1 << 4, bit_5 = 1 << 5, bit_6 = 1 << 6, bit_7 = 1 << 7,
            bit_8 = 1 << 8, bit_9 = 1 << 9, bit_10 = 1 << 10, bit_11 = 1 << 11,
            bit_12 = 1 << 12, bit_13 = 1 << 13, bit_14 = 1 << 14, bit_15 = 1 << 15,

            bit_16 = 1 << 16, bit_17 = 1 << 17, bit_18 = 1 << 18, bit_19 = 1 << 19,
            bit_20 = 1 << 20, bit_21 = 1 << 21, bit_22 = 1 << 22, bit_23 = 1 << 23,
            bit_24 = 1 << 24, bit_25 = 1 << 25, bit_26 = 1 << 26, bit_27 = 1 << 27,
            bit_28 = 1 << 28, bit_29 = 1 << 29, bit_30 = 1 << 30, bit_31 = 1 << 31,

            All = ~0
        }

        FlagsEnumerator flags;

        public override void OnInspectorGUI()
        {
            BitMask32Variable var = (BitMask32Variable)target;
            EditorGUILayout.LabelField(var.Value.ToString());
            flags = (FlagsEnumerator)(uint)var.Value;
            var.Value = (uint)(FlagsEnumerator)EditorGUILayout.EnumFlagsField(flags);

            var.isLocked = EditorGUILayout.Toggle("Locked: ", var.isLocked);

            if (GUI.changed) EditorUtility.SetDirty(var);
        }
    }
}