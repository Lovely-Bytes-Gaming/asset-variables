using System.IO;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    public interface IStringSerializable
    {
        string GetKey();
        string Serialize(StreamWriter streamWriter);
        void Deserialize(in string stringRepresentation);
    }
}
