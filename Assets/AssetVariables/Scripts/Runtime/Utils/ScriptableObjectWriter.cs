using System.Collections.Generic;
using System.IO;
using TNRD;
using UnityEngine;
using System.Linq;

namespace LovelyBytes.AssetVariables
{
    public abstract class ScriptableObjectWriter : ScriptableObject
    {
        [SerializeField] 
        private string _relativePath = string.Empty;
        
        [SerializeField] 
        private List<SerializableInterface<IStringSerializable>> _serializables;
        
        private string Path => $"{Application.persistentDataPath}/{_relativePath}";
        
        public void Save()
        {
            if (string.IsNullOrEmpty(_relativePath))
            {
                Debug.LogError($"{name}: could not save scriptable objects: relative path is empty");
                return;
            }
            
            List<IStringSerializable> list = ConvertFrom(_serializables);
            
            ProcessFileStream(stream =>
            {
                using StreamWriter streamWriter = new(stream);
                WriteItems(list, streamWriter);
            });
        }

        public void Load()
        {
            if (string.IsNullOrEmpty(_relativePath))
            {
                Debug.LogError($"{name}: could not load scriptable objects: relative path is empty");
                return;
            }

            List<IStringSerializable> list = ConvertFrom(_serializables);
            
            ProcessFileStream(stream =>
            {
                using StreamReader streamReader = new(stream);
                ReadItems(list, streamReader);
            });
        }
        
        protected abstract void WriteItems(List<IStringSerializable> items, StreamWriter streamWriter);
        protected abstract void ReadItems(List<IStringSerializable> items, StreamReader streamReader);

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

        private List<IStringSerializable> ConvertFrom(IEnumerable<SerializableInterface<IStringSerializable>> input)
        {
            return input.Select(item => item?.Value).ToList();
        }
    }
}
