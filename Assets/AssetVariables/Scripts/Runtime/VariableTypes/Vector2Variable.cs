using UnityEngine;


namespace LovelyBytesGaming.AssetVariables
{
    [CreateAssetMenu(menuName = "LovelyBytesGaming/AssetVariables/Vector2")]
    public class Vector2Variable : Variable<Vector2> 
    { 
        public void Reset() => Value = Vector2.zero;
    };
}
