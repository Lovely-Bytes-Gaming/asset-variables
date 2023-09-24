using UnityEngine;

namespace LovelyBytesGaming.AssetVariables
{
    [System.Serializable]
    public struct Foo
    {
        public bool One;
		public int Two;
		

        public override string ToString()
            => JsonUtility.ToJson(this);
    }

    [CreateAssetMenu(menuName = "LovelyBytesGaming/AssetVariables/Foo")]
    public class FooVariable : Variable<Foo>
    {
        public void Reset()
            => Value = new Foo();
    }
}
