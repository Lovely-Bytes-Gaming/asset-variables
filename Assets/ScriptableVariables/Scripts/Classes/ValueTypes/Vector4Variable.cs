using UnityEngine;


namespace CustomLibs.Util.ScriptableVariables
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Value Types/Vector4")]
    public class Vector4Variable : ValueType<Vector4> 
    { 
        void Reset() => m_Value = Vector4.zero;
    };
}
