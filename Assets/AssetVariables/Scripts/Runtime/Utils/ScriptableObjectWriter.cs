using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    [CreateAssetMenu(menuName = AssetVariableConstants.DefaultAssetPath + "Scriptable Object Writer")]    
    public class ScriptableObjectWriter : ScriptableObject
    {
        [SerializeField] 
        private string _relativePath = string.Empty;
        
        [SerializeField] 
        private List<StringSerializable> _serializables;

        private const string _separator = "::";
        
        private string Path => $"{Application.persistentDataPath}/{_relativePath}";
        
        public void Save()
        {
            if (string.IsNullOrEmpty(_relativePath))
            {
                Debug.LogError($"{name}: could not save scriptable objects: relative path is empty");
                return;
            }
            
            FileStream stream = null;

            try
            {
                stream = new FileStream(_relativePath, FileMode.OpenOrCreate);

                using (StreamWriter streamWriter = new(stream))
                {
                    WriteItems(_serializables, streamWriter);
                }
            }
            finally
            {
                stream?.Dispose();
            }
        }

        public void Load()
        {
            if (string.IsNullOrEmpty(_relativePath))
            {
                Debug.LogError($"{name}: could not load scriptable objects: relative path is empty");
                return;
            }
            
            FileStream stream = null;
            
            try
            {
                stream = new FileStream(_relativePath, FileMode.OpenOrCreate);

                using (StreamReader streamReader = new(stream))
                {
                    ReadItems(_serializables, streamReader);
                }
            }
            finally
            {
                stream?.Dispose();
            }
        }
        
        protected virtual void WriteItems(List<StringSerializable> items, StreamWriter streamWriter)
        {
            foreach (StringSerializable item in items)
            {
                string content = $"{item.GetKey()}{_separator}{item.Serialize(streamWriter)}";
                streamWriter.WriteLine(content);
            }
        }

        protected virtual void ReadItems(List<StringSerializable> items, StreamReader streamReader)
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

            foreach (StringSerializable item in items)
            {
                string key = item.GetKey();

                if (!pairs.TryGetValue(key, out string value)) 
                    continue;
                
                item.Deserialize(value);
            }
        }
    }
}
