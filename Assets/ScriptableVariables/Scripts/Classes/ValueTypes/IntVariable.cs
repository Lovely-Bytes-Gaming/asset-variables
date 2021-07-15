using UnityEngine;


namespace CustomLibs.Util.ScriptableVariables
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Value Types/Int")]
    public class IntVariable : ValueType<int> 
    { 
        void Reset() => m_Value = 0;
    };
}
