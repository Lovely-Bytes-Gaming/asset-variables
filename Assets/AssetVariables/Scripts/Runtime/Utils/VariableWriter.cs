using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    [CreateAssetMenu(menuName = AssetVariableConstants.DefaultAssetPath + "Variable Writer")]    
    public class VariableWriter : ScriptableObject
    {
        [SerializeField] 
        private List<StringSerializable> _persistentVariables;

        public void Save()
        {
            StreamWriter streamWriter = new(Application.persistentDataPath + "/variables.dat");
            
            foreach (StringSerializable variable in _persistentVariables)
            {
                string content = variable.Serialize(streamWriter);
                streamWriter.WriteLine(content);
            }
        }

        public void Load()
        {
            
        }
    }
}
