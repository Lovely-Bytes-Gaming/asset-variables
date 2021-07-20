using UnityEngine;


namespace CustomLibrary.Util.ScriptableVariables
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Value Types/Vector4")]
    public class Vector4Variable : ValueType<Vector4> 
    { 
        public void Reset() => Value = Vector4.zero;
    };
}
