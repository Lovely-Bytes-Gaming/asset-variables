using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Range/Float Range")]
public class FloatRange : RangeType<float> 
{ 
    void Reset()
    {
        m_Min = 0f;
        m_Max = 1f;
        m_Value = 0f;
    }
};