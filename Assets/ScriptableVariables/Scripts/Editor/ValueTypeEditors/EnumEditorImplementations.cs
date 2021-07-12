using UnityEditor;

/*
 add an editor definition for your custom enum in the manner of the example below.
 */

namespace CustomLibs.Util.ScriptableVariables
{
    [CustomEditor(typeof(ExampleEnumVariable))]
    public class ExampleEnumEditor : EnumTypeEditor<ExampleEnum> { }
}