using UnityEngine;


namespace CustomLibrary.Util.ScriptableVariables
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Value Types/Vector2")]
    public class Vector2Variable : ValueType<Vector2> 
    { 
        void Reset() => m_Value = Vector2.zero;
    };
}
