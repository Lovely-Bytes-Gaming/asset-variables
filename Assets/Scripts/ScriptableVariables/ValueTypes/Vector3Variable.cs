using UnityEngine;


namespace CustomLibs.Util.ScriptableVariables
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Value Types/Vector3")]
    public class Vector3Variable : ValueType<Vector3> 
    { 
        void Reset()
        {
            m_Value = Vector3.zero;
        }
    };
}