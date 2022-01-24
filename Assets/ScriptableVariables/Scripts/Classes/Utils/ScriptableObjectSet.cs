using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Scriptable Object Set")]
public class ScriptableObjectSet : ScriptableObject, ISerializationCallbackReceiver
{
    private Dictionary<string, ScriptableObject> m_objectDict;
    private Dictionary<string, ScriptableObject> ObjectDict
        => m_objectDict ?? (m_objectDict = new Dictionary<string, ScriptableObject>());

    [SerializeField, HideInInspector]
    private List<ScriptableObject> m_values = new List<ScriptableObject>();

    [SerializeField, HideInInspector]
    private List<string> m_keys = new List<string>();

    public void Add(ScriptableObject scriptableObject)
    {
        string objectName = scriptableObject.name;
        if (!ObjectDict.ContainsKey(objectName))
        {
            ObjectDict.Add(objectName, scriptableObject);
        }
    }

    public void GetObject<T>(string name, out T objRef) where T : ScriptableObject
    {
        if (!ObjectDict.TryGetValue(name, out ScriptableObject objHandle))
        {
            throw new Exception($"[IFG::UTIL] {this.name} does not contain a value with name {name}");
        }
        objRef = objHandle as T;
    }

    public ICollection<ScriptableObject> GetAll()
    {
        return ObjectDict.Values;
    }

    public void OnBeforeSerialize()
    {
        m_keys.Clear();
        m_values.Clear();

        foreach(var kv_pair in ObjectDict)
        {
            m_keys.Add(kv_pair.Key);
            m_values.Add(kv_pair.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        if (m_keys.Count != m_values.Count)
            throw new Exception("[IFG::UTIL: Incompatible number of values and keys after deserialization]");

        for(int i = 0; i < m_keys.Count; i++)
        {
            ObjectDict.Add(m_keys[i], m_values[i]);
        }
    }
}