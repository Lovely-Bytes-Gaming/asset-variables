using System.Collections.Generic;
using System.IO;
using TNRD;
using UnityEngine;
using System.Linq;
using UnityEngine.Serialization;

namespace LovelyBytes.AssetVariables
{
    public abstract class ScriptableObjectWriter : ScriptableObject
    {
        [SerializeField] 
        private string _relativePath = string.Empty;
        
        [SerializeField] 
        private List<SerializableInterface<IStringSerializable>> _objectsToSave;

        private List<IStringSerializable> _unpackedObjects = new();
        
        private string Path => $"{Application.persistentDataPath}/{_relativePath}";
        
        public void Save()
        {
            if (string.IsNullOrEmpty(_relativePath))
            {
                Debug.LogError($"{name}: could not save objects: relative path is empty");
                return;
            }
            
            Unpack(_objectsToSave, _unpackedObjects);
            
            ProcessFileStream(stream =>
            {
                using StreamWriter streamWriter = new(stream);
                WriteItems(_unpackedObjects, streamWriter);
            });
        }

        public void Load()
        {
            if (string.IsNullOrEmpty(_relativePath))
            {
                Debug.LogError($"{name}: could not load scriptable objects: relative path is empty");
                return;
            }

            Unpack(_objectsToSave, _unpackedObjects);
            
            ProcessFileStream(stream =>
            {
                using StreamReader streamReader = new(stream);
                ReadItems(_unpackedObjects, streamReader);
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

        private void Unpack(List<SerializableInterface<IStringSerializable>> input, List<IStringSerializable> output)
        {
            output.Clear();

            foreach (SerializableInterface<IStringSerializable> item in input)
            {
                if (item.Value is not Object)
                    continue;
                
                output.Add(item.Value);
            }
        }
    }
}
