using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Value Types/Vector3 Int")]
public class Vector3IntVariable : ValueType<Vector3Int> 
{ 
    void Reset()
    {
        m_Value = Vector3Int.zero;
    }
};