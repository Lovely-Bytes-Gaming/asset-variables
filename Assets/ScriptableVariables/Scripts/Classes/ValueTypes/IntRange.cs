using UnityEngine;


namespace CustomLibrary.Util.ScriptableVariables
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Range/Int Range")]
    public class IntRange : RangeType<int> 
    {
        public void Reset()
        {
            m_Min = 0;
            m_Max = 100;
            Value = 0;
        }
    };
}