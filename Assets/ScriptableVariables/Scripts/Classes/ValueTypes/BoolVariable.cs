using UnityEngine;


namespace CustomLibs.Util.ScriptableVariables
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Value Types/Bool")]
    public class BoolVariable : ValueType<bool> 
    {
        void Reset()
        {
            m_Value = false;
        }
    };
}