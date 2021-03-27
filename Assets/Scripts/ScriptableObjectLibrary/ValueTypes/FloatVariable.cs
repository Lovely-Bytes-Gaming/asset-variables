using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Value Types/Float")]
public class FloatVariable : ValueType<float> 
{
    void Reset()
    {
        m_Value = 0f;
    }
};
