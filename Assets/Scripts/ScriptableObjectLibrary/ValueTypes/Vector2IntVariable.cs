using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Value Types/Vector2 Int")]
public class Vector2IntVariable : ValueType<Vector2Int> 
{ 
    void Reset()
    {
        m_Value = Vector2Int.zero;
    }
};
