using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Value Types/Quaternion")]
public class QuaternionVariable : ValueType<Quaternion> 
{ 
    void Reset()
    {
        m_Value = Quaternion.identity;
    }
}