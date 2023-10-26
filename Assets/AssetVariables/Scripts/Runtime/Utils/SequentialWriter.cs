using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    [CreateAssetMenu(menuName = AssetVariableConstants.DefaultAssetPath + "Scriptable Object Writer/Sequential Writer")]    
    public class SequentialWriter : ScriptableObjectWriter
    {
        private const string _separator = "::";
        
        protected override void WriteItems(List<IStringSerializable> items, StreamWriter streamWriter)
        {
            foreach (IStringSerializable item in items)
            {
                string content = $"{item.GetKey()}{_separator}{item.Serialize(streamWriter)}";
                streamWriter.WriteLine(content);
            }
        }

        protected override void ReadItems(List<IStringSerializable> items, StreamReader streamReader)
        {
            Dictionary<string, string> pairs = new();
            while (!streamReader.EndOfStream)
            {
                string nextLine = streamReader.ReadLine();
                
                if (nextLine == null)
                    continue;

                int separatorStartIndex = nextLine.IndexOf(_separator, StringComparison.Ordinal);
                int separatorEndIndex = separatorStartIndex + _separator.Length;
                
                if (separatorStartIndex < 0 || separatorEndIndex >= nextLine.Length)
                    continue;
                
                string key = nextLine[..separatorStartIndex];
                string value = nextLine[(separatorEndIndex)..];
                
                pairs.Add(key, value);
            }

            foreach (IStringSerializable item in items)
            {
                string key = item.GetKey();

                if (!pairs.TryGetValue(key, out string value)) 
                    continue;
                
                item.Deserialize(value);
            }
        }
    }
}
