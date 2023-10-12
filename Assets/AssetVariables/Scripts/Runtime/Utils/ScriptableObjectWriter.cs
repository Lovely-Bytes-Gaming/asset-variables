using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    public abstract class ScriptableObjectWriter : ScriptableObject
    {
        [SerializeField] 
        private string _relativePath = string.Empty;
        
        [SerializeField] 
        private List<StringSerializable> _serializables;
        
        private string Path => $"{Application.persistentDataPath}/{_relativePath}";
        
        public void Save()
        {
            if (string.IsNullOrEmpty(_relativePath))
            {
                Debug.LogError($"{name}: could not save scriptable objects: relative path is empty");
                return;
            }
            
            ProcessFileStream(stream =>
            {
                using StreamWriter streamWriter = new(stream);
                WriteItems(_serializables, streamWriter);
            });
        }

        public void Load()
        {
            if (string.IsNullOrEmpty(_relativePath))
            {
                Debug.LogError($"{name}: could not load scriptable objects: relative path is empty");
                return;
            }
            
            ProcessFileStream(stream =>
            {
                using StreamReader streamReader = new(stream);
                ReadItems(_serializables, streamReader);
            });
        }
        
        protected abstract void WriteItems(List<StringSerializable> items, StreamWriter streamWriter);
        protected abstract void ReadItems(List<StringSerializable> items, StreamReader streamReader);

        private void ProcessFileStream(System.Action<FileStream> streamAction)
        {
            FileStream stream = null;

            try
            {
                stream = new FileStream(Path, FileMode.OpenOrCreate);
                streamAction?.Invoke(stream);
            }
            finally
            {
                stream?.Dispose();
            }
        }
    }
}
