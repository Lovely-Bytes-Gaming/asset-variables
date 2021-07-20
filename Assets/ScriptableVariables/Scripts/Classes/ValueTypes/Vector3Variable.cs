using UnityEngine;


namespace CustomLibrary.Util.ScriptableVariables
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Value Types/Vector3")]
    public class Vector3Variable : ValueType<Vector3> 
    { 
        public void Reset() => Value = Vector3.zero;
    };
}