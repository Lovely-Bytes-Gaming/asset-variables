using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    [CreateAssetMenu(menuName = AssetVariableConstants.DefaultAssetPath + "Variable Writer")]    
    public class VariableWriter : ScriptableObject
    {
        private static string FileName => Application.persistentDataPath + "/variables.dat";
        
        [SerializeField] 
        private List<StringSerializable> _persistentVariables;
        
        public void Save()
        {
            FileStream stream = null;

            try
            {
                stream = new FileStream(FileName, FileMode.OpenOrCreate);

                using (StreamWriter streamWriter = new(stream))
                {
                    foreach (StringSerializable variable in _persistentVariables)
                    {
                        string content = variable.GetStringRepresentation();
                        streamWriter.WriteLine(content);
                    }
                }
            }
            finally
            {
                stream?.Dispose();
            }
        }

        public void Load()
        {
            FileStream stream = null;
            
            try
            {
                stream = new FileStream(FileName, FileMode.OpenOrCreate);

                using (StreamReader streamReader = new(stream))
                {
                    int i = 0;
                    while (!streamReader.EndOfStream && i < _persistentVariables.Count)
                    {
                        string nextLine = streamReader.ReadLine();
                        _persistentVariables[i].InitializeFromString(nextLine);
                        ++i;
                    }                   
                }
            }
            finally
            {
                stream?.Dispose();
            }
        }
    }
}
