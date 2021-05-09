using UnityEngine;

/*
    This is an example of how to implement a scriptable enum.
    When implementing a new scriptable enum, don't forget to also add
    a custom editor definition in 'EnumEditorImplementations.cs'
 */


namespace CustomLibs.Util.ScriptableVariables
{
    public enum ExampleEnum
    {
        FOO,
        BAR,
        BAZ
    }

    [CreateAssetMenu(menuName = "Scriptable Objects/Enum Types/Example Enum")]
    public class ExampleEnumVariable : EnumType<ExampleEnum>
    {
        void Reset()
        {
            m_Value = ExampleEnum.FOO;
        }
    }
}
