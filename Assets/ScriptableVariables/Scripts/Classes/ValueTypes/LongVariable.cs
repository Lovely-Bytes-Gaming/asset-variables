using UnityEngine;


namespace CustomLibs.Util.ScriptableVariables
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Value Types/Long")]
    public class LongVariable : ValueType<long> 
    {
        void Reset()
        {
            m_Value = 0;
        }
    };
}