using UnityEngine;


namespace CustomLibs.Util.ScriptableVariables
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Range/Int Range")]
    public class IntRange : RangeType<int> 
    {
        void Reset()
        {
            m_Min = 0;
            m_Max = 100;
            m_Value = 0;
        }
    };
}