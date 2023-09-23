using UnityEngine;


namespace LovelyBytesGaming.AssetVariables
{
    [CreateAssetMenu(menuName = "LovelyBytesGaming/AssetVariables/Quaternion")]
    public class QuaternionVariable : Variable<Quaternion> 
    { 
        public void Reset() => Value = Quaternion.identity;
    }
}